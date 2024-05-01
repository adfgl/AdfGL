using AdfGLCoreLib;
using AdfGLCoreLib.Enums;
using AdfGLCoreLib.Services;
using System.Diagnostics;

namespace AdfGL_SDL2
{
    using static SDL2.SDL;

    public class WindowSDL2
    {
        uint s_id = 0;
        IntPtr s_window;
        IntPtr s_texture;
        IntPtr s_renderer;
        IntPtr s_ptrCursor;
        EMouseCursor s_currentCursor = EMouseCursor.Arrow;

        static IntPtr BuildTexture(IntPtr renderer, int width, int height)
        {
            return SDL_CreateTexture(renderer, SDL_PIXELFORMAT_ARGB8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, width, height);
        }

        static IntPtr BuildRenderer(IntPtr window)
        {
            var ptr = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_SOFTWARE); // SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC SDL_RENDERER_SOFTWARE
            SDL_SetRenderDrawBlendMode(ptr, SDL_BlendMode.SDL_BLENDMODE_BLEND);
            return ptr;
        }

        public WindowSDL2 Create(string title, int width, int height)
        {
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                throw new InvalidProgramException($"There was an issue initializing SDL. {SDL_GetError()}");
            }

            SDL_WindowFlags winFlags = SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

            s_window = SDL_CreateWindow(
                   title: title,
                   x: SDL_WINDOWPOS_CENTERED,
                   y: SDL_WINDOWPOS_CENTERED,
                   w: width,
                   h: height,
                   flags: winFlags);

            if (s_window != IntPtr.Zero)
            {
                s_renderer = BuildRenderer(s_window);
                if (s_renderer != IntPtr.Zero)
                {
                    s_id = SDL_GetWindowID(s_window);
                    s_texture = BuildTexture(s_renderer, width, height);
                }
                else
                {
                    SDL_DestroyWindow(s_window);
                    s_window = IntPtr.Zero;
                    throw new InvalidProgramException($"There was an issue creating the renderer. {SDL_GetError()}");
                }
            }
            else
            {
                throw new InvalidProgramException($"There was an issue creating the window. {SDL_GetError()}");
            }
            return this;
        }

        public void Destroy()
        {
            SDL_DestroyWindow(s_window);
            SDL_RenderClear(s_renderer);
            SDL_DestroyRenderer(s_renderer);
            SDL_DestroyTexture(s_texture);
            SDL_FreeCursor(s_ptrCursor);
        }

        public void SetMouseCursor(EMouseCursor cursor)
        {
            s_currentCursor = cursor;

            SDL_SystemCursor sdlCursor;
            switch (cursor)
            {
                case EMouseCursor.None:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_ARROW;
                    break;
                case EMouseCursor.Arrow:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_ARROW;
                    break;
                case EMouseCursor.IBeam:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_IBEAM;
                    break;
                case EMouseCursor.Wait:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_WAIT;
                    break;
                case EMouseCursor.Crosshair:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_CROSSHAIR;
                    break;
                case EMouseCursor.WaitArrow:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_WAITARROW;
                    break;
                case EMouseCursor.SizeNWSE:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZENWSE;
                    break;
                case EMouseCursor.SizeNESW:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZENESW;
                    break;
                case EMouseCursor.SizeWE:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZEWE;
                    break;
                case EMouseCursor.SizeNS:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZENS;
                    break;
                case EMouseCursor.SizeAll:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZEALL;
                    break;
                case EMouseCursor.No:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_NO;
                    break;
                case EMouseCursor.Hand:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_HAND;
                    break;
                default:
                    sdlCursor = SDL_SystemCursor.SDL_SYSTEM_CURSOR_ARROW;
                    break;
            }

            s_ptrCursor = SDL_CreateSystemCursor(sdlCursor);
            SDL_SetCursor(s_ptrCursor);
        }


        int s_widthBefore = 0;
        int s_heightBefore = 0;
        bool s_forceRedraw = false;

        public void Start(AppHost host)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int previous = DateTime.Now.Millisecond;
            int current, elapsed;
            int lag = 0;

            int framesRendered = 0;
            int frameTimer = 0;

            GetSize(out s_widthBefore, out s_heightBefore);

            host.Start();
            while (host.IsRunning)
            {
                int targetFrameTime = 1000 / host.Application.TargetFps;
                current = DateTime.Now.Millisecond;
                elapsed = Math.Abs(current - previous); // why tf i get negative values?..
                previous = current;

                if (TikTakService.TikTak.IsRunning)
                {
                    TikTakService.TikTak.Ms += elapsed;
                }

                if (SDL_PollEvent(out SDL_Event e) != 0)
                {
                    HandleEvent(e, host);
                }

                if (lag > targetFrameTime || s_forceRedraw)
                {
                    Update(host, elapsed);
                    Render(host);

                    framesRendered++;
                    lag = 0;
                    s_forceRedraw = false;
                }
                else
                {
                    lag += elapsed;
                }

                if (frameTimer + elapsed <= 1000)
                {
                    frameTimer += elapsed;
                }
                else
                {
                    host.Application.ActualFps = framesRendered;
                    frameTimer = 0;
                    framesRendered = 0;
                }
            }
        }

        void HandleEvent(SDL_Event e, AppHost host)
        {
            GetMouse(out int x, out int y);
            switch (e.type)
            {
                case SDL_EventType.SDL_WINDOWEVENT:
                    switch (e.window.windowEvent)
                    {
                        case SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                            host.Stop();
                            return;

                        case SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN:
                            host.SendEvent(AppEventBuilder.Show());
                            break;

                        case SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN:
                            host.SendEvent(AppEventBuilder.Hide());
                            break;

                        case SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                            host.SendEvent(AppEventBuilder.Minimize());
                            break;

                        case SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                            host.SendEvent(AppEventBuilder.Maximize());
                            break;

                        case SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                            host.SendEvent(AppEventBuilder.Restore());
                            break;

                        case SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:
                            host.SendEvent(AppEventBuilder.MouseEnter());
                            break;

                        case SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                            host.SendEvent(AppEventBuilder.MouseLeave());
                            break;

                        case SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                            int newWidth = e.window.data1;
                            int newHeight = e.window.data2;

                            host.HandleResize(newWidth, newHeight);
                            HandleResize(newWidth, newHeight);
                            host.SendEvent(AppEventBuilder.SizeChanged(s_widthBefore, s_heightBefore, newWidth, newHeight));

                            s_widthBefore = newWidth;
                            s_heightBefore = newHeight;
                            s_forceRedraw = true;
                            break;

                        case SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:

                            break;

                        default:
                            break;
                    }


                    break;

                case SDL_EventType.SDL_MOUSEMOTION:
                    host.SendEvent(AppEventBuilder.MouseMove(x, y));
                    break;

                case SDL_EventType.SDL_KEYDOWN:
                    host.SendEvent(AppEventBuilder.KeyDown(ToGfxKey(e.key.keysym.scancode)));
                    break;
                case SDL_EventType.SDL_KEYUP:
                    host.SendEvent(AppEventBuilder.KeyUp(ToGfxKey(e.key.keysym.scancode)));
                    break;

                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    host.SendEvent(AppEventBuilder.MouseDown(ToGfxButton(e.button.button), x, y, e.button.clicks));
                    break;
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    host.SendEvent(AppEventBuilder.MouseUp(ToGfxButton(e.button.button), x, y));
                    break;
                case SDL_EventType.SDL_MOUSEWHEEL:
                    host.SendEvent(AppEventBuilder.MouseWheel(e.wheel.x, e.wheel.y, x, y));
                    break;

                default:
                    break;
            }
        }

        EKeyCode ToGfxKey(SDL_Scancode key)
        {
            return key switch
            {
                SDL_Scancode.SDL_SCANCODE_RETURN => EKeyCode.RETURN,
                SDL_Scancode.SDL_SCANCODE_ESCAPE => EKeyCode.ESCAPE,
                SDL_Scancode.SDL_SCANCODE_BACKSPACE => EKeyCode.BACKSPACE,
                SDL_Scancode.SDL_SCANCODE_TAB => EKeyCode.TAB,
                SDL_Scancode.SDL_SCANCODE_SPACE => EKeyCode.SPACE,
                SDL_Scancode.SDL_SCANCODE_DELETE => EKeyCode.DELETE,

                SDL_Scancode.SDL_SCANCODE_0 => EKeyCode.N_0,
                SDL_Scancode.SDL_SCANCODE_1 => EKeyCode.N_1,
                SDL_Scancode.SDL_SCANCODE_2 => EKeyCode.N_2,
                SDL_Scancode.SDL_SCANCODE_3 => EKeyCode.N_3,
                SDL_Scancode.SDL_SCANCODE_4 => EKeyCode.N_4,
                SDL_Scancode.SDL_SCANCODE_5 => EKeyCode.N_5,
                SDL_Scancode.SDL_SCANCODE_6 => EKeyCode.N_6,
                SDL_Scancode.SDL_SCANCODE_7 => EKeyCode.N_7,
                SDL_Scancode.SDL_SCANCODE_8 => EKeyCode.N_8,
                SDL_Scancode.SDL_SCANCODE_9 => EKeyCode.N_9,

                SDL_Scancode.SDL_SCANCODE_UP => EKeyCode.UP,
                SDL_Scancode.SDL_SCANCODE_DOWN => EKeyCode.DOWN,
                SDL_Scancode.SDL_SCANCODE_LEFT => EKeyCode.LEFT,
                SDL_Scancode.SDL_SCANCODE_RIGHT => EKeyCode.RIGHT,

                SDL_Scancode.SDL_SCANCODE_A => EKeyCode.a,
                SDL_Scancode.SDL_SCANCODE_B => EKeyCode.b,
                SDL_Scancode.SDL_SCANCODE_C => EKeyCode.c,
                SDL_Scancode.SDL_SCANCODE_D => EKeyCode.d,
                SDL_Scancode.SDL_SCANCODE_E => EKeyCode.e,
                SDL_Scancode.SDL_SCANCODE_F => EKeyCode.f,
                SDL_Scancode.SDL_SCANCODE_G => EKeyCode.g,
                SDL_Scancode.SDL_SCANCODE_H => EKeyCode.h,
                SDL_Scancode.SDL_SCANCODE_I => EKeyCode.i,
                SDL_Scancode.SDL_SCANCODE_J => EKeyCode.j,
                SDL_Scancode.SDL_SCANCODE_K => EKeyCode.k,
                SDL_Scancode.SDL_SCANCODE_L => EKeyCode.l,
                SDL_Scancode.SDL_SCANCODE_M => EKeyCode.m,
                SDL_Scancode.SDL_SCANCODE_N => EKeyCode.n,
                SDL_Scancode.SDL_SCANCODE_O => EKeyCode.o,
                SDL_Scancode.SDL_SCANCODE_P => EKeyCode.p,
                SDL_Scancode.SDL_SCANCODE_Q => EKeyCode.q,
                SDL_Scancode.SDL_SCANCODE_R => EKeyCode.r,
                SDL_Scancode.SDL_SCANCODE_S => EKeyCode.s,
                SDL_Scancode.SDL_SCANCODE_T => EKeyCode.t,
                SDL_Scancode.SDL_SCANCODE_U => EKeyCode.u,
                SDL_Scancode.SDL_SCANCODE_V => EKeyCode.v,
                SDL_Scancode.SDL_SCANCODE_W => EKeyCode.w,
                SDL_Scancode.SDL_SCANCODE_X => EKeyCode.x,
                SDL_Scancode.SDL_SCANCODE_Y => EKeyCode.y,
                SDL_Scancode.SDL_SCANCODE_Z => EKeyCode.z,

                SDL_Scancode.SDL_SCANCODE_LCTRL => EKeyCode.LCTRL,
                SDL_Scancode.SDL_SCANCODE_LSHIFT => EKeyCode.LSHIFT,
                SDL_Scancode.SDL_SCANCODE_RCTRL => EKeyCode.RCTRL,
                SDL_Scancode.SDL_SCANCODE_RSHIFT => EKeyCode.RSHIFT,

                _ => EKeyCode.UNKNOWN,
            };
        }

        EMouseButton ToGfxButton(byte button)
        {
            switch (button)
            {
                case 1: return EMouseButton.Left;
                case 3: return EMouseButton.Right;
                case 2: return EMouseButton.Middle;
                case 4: return EMouseButton.Previous;
                case 5: return EMouseButton.Next;

                default:
                    return EMouseButton.Undefined;
            }
        }


        void Update(AppHost host, float ellapse)
        {
            host.Application.Update(ellapse);
        }

        void Render(AppHost host)
        {
            host.RenderToBuffer();
            host.UpdateScreen(host.GameLoop.GetBuffer());
        }

        void HandleResize(int width, int height)
        {
            SDL_DestroyRenderer(s_renderer);
            SDL_DestroyTexture(s_texture);
            s_renderer = BuildRenderer(s_window);
            s_texture = BuildTexture(s_renderer, width, height);
        }

        public void SetPixelsToScreen(int width, int height, ReadOnlySpan<byte> pixels)
        {
            SDL_Rect rect = new SDL_Rect() { x = 0, y = 0, w = width, h = height };
            IntPtr ptrPixels;
            unsafe
            {
                fixed (byte* ptr = pixels)
                {
                    ptrPixels = new IntPtr(ptr);
                }
            }

            SDL_UpdateTexture(s_texture, ref rect, ptrPixels, width * 4); // 4 - bytes per pixel
            SDL_RenderClear(s_renderer);
            SDL_RenderCopy(s_renderer, s_texture, IntPtr.Zero, IntPtr.Zero);
            SDL_RenderPresent(s_renderer);
        }

        public void GetMouse(out int x, out int y)
        {
            SDL_GetMouseState(out x, out y);
        }

        public void GetSize(out int width, out int height)
        {
            SDL_GetWindowSize(s_window, out width, out height);
        }
        public void SetSize(int width, int height)
        {
            SDL_SetWindowSize(s_window, width, height);
        }

    }
}
