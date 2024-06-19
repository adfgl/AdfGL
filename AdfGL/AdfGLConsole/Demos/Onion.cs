﻿using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGLConsole.Demos
{
    public static class Onion
    {
        public static Vec3[] Points => new Vec3[]
            {
           new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (4753461, -818930444, 6545000),
new (3762312, -2092654660, 5174223),
new (2618270, -3571459396, 4355294),
new (212577, -6629720571, 3594210),
new (-2560179, -9876448900, 2792129),
new (-4688850, -12009347202, 1961249),
new (-5099760, -12339767485, 1927719),
new (-5608935, -12717228323, 1897189),
new (-6192421, -13115257296, 1867554),
new (-6881456, -13537195644, 1837920),
new (-7756370, -13986338884, 1808285),
new (-8835132, -14420486918, 1778651),
new (-10345086, -14657863164, 2388927),
new (-13015731, -13736459797, 801622),
new (-16737709, -11829402689, -785683),
new (-22036784, -7697909260, -2353340),
new (-26287501, -3141232308, -3809809),
new (-27689200, -11048096, -4734898),
new (-26280442, 3120402669, -5082867),
new (-22030021, 7687761973, -4341387),
new (-16728476, 11826760089, -3549563),
new (-13004424, 13741066879, -2749428),
new (-10327535, 14665430599, -2846858),
new (-8819676, 14405381273, -2891097),
new (-7738839, 13969791946, -2818863),
new (-6861851, 13519207413, -2746628),
new (-6170742, 13095827772, -2674394),
new (-5585184, 12696361190, -2601316),
new (-5074014, 12317540488, -2521407),
new (-4661163, 11985813976, -2441002),
new (-2535448, 9861079389, -798971),
new (235789, 6620861039, 675294),
new (2639733, 3569615602, 2299737),
new (3781629, 2098249287, 3924180),
new (4753461, 849473777, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (4868452, -664843287, -8805911),
new (3920852, -1888464028, -19326558),
new (2891572, -3212460083, -30752525),
new (739384, -5973851332, -63214435),
new (-2029740, -9270826229, -72981271),
new (-4378466, -11734664215, -52118223),
new (-4816125, -12096845282, -51342350),
new (-5329618, -12493924706, -50954805),
new (-5936595, -12924498925, -50261067),
new (-6668249, -13386753409, -49210217),
new (-7562554, -13867491885, -49102069),
new (-8688763, -14338882148, -48339165),
new (-10274696, -14589707220, -50858330),
new (-13115478, -13596613907, -58143661),
new (-16922154, -11661531288, -56995596),
new (-22351190, -7358246561, -52355109),
new (-26372800, -3028273872, -20608176),
new (-27689200, -11048096, -4734898),
new (-26365481, 3007444437, 11718070),
new (-22344173, 7348224161, 45441470),
new (-16912580, 11659062705, 52369197),
new (-13100210, 13597753059, 56727912),
new (-10253066, 14594057260, 51412834),
new (-8672728, 14323497268, 47722868),
new (-7544364, 13850626921, 48654379),
new (-6647905, 13368408362, 48931135),
new (-5914129, 12904706722, 50142364),
new (-5305045, 12472700749, 50992322),
new (-4789505, 12074248841, 51544982),
new (-4349847, 11710742356, 52492103),
new (-2004172, 9255104671, 75738391),
new (763507, 5964541408, 68277190),
new (2913849, 3210222460, 38097236),
new (3940902, 1893704209, 29045673),
new (4868452, 695386620, 21895911),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (4982292, -512296750, -24003259),
new (4077809, -1686313980, -43582244),
new (3088591, -2957098417, -55273779),
new (1222635, -5359859393, -121797361),
new (-1478200, -8623021970, -151576385),
new (-3964196, -11311251199, -129891261),
new (-4445393, -11734501920, -123328635),
new (-5002072, -12193265595, -116640745),
new (-5656293, -12688792486, -109558340),
new (-6435417, -13212733282, -103615945),
new (-7363205, -13731856452, -102466042),
new (-8534757, -14242643688, -100931436),
new (-10242876, -14447120200, -118728075),
new (-13188766, -13432417508, -123433320),
new (-17110537, -11480915149, -115500769),
new (-22644851, -7036239937, -99539771),
new (-26437152, -2899086165, -39483560),
new (-27689200, -11048096, -4734898),
new (-26429577, 2878257286, 30595736),
new (-22637581, 7026342244, 92409007),
new (-17100501, 11478486052, 110618496),
new (-13172530, 13433451585, 122056576),
new (-10220824, 14452104995, 119433958),
new (-8518149, 14226982543, 100805940),
new (-7344376, 13714690431, 102571984),
new (-6414404, 13194097540, 103943357),
new (-5633104, 12668695387, 110102855),
new (-4976719, 12171716818, 117398296),
new (-4417934, 11711558674, 124306040),
new (-3934684, 11286966621, 131094432),
new (-1451829, 8606970653, 155076360),
new (1247581, 5350187806, 127626519),
new (3111673, 2954470842, 63301275),
new (4098583, 1691203203, 53915865),
new (4982292, 542840084, 37093259),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5096475, -359291860, -38636767),
new (4243127, -1473394672, -69129852),
new (3281439, -2708062319, -81949243),
new (1668501, -4784612537, -172167843),
new (-886728, -7908324406, -233611403),
new (-3425059, -10716770102, -221921885),
new (-3958055, -11214402869, -212844246),
new (-4567845, -11749338155, -203308291),
new (-5267223, -12308578827, -193365575),
new (-6090939, -12893330587, -183236203),
new (-7093000, -13501510668, -173812797),
new (-8370839, -14075523898, -169766519),
new (-10236001, -14237056998, -196285983),
new (-13266296, -13245099437, -196251905),
new (-17305564, -11258453578, -184038762),
new (-22935219, -6712243474, -146835929),
new (-26496019, -2761318094, -58742170),
new (-27689200, -11048096, -4734898),
new (-26488174, 2740490178, 49856472),
new (-22927683, 6702478265, 139476053),
new (-17291593, 11252640402, 179918024),
new (-13249173, 13246160900, 194885557),
new (-10213502, 14242709415, 197151114),
new (-8347430, 14065822120, 168341386),
new (-7073551, 13484078973, 174486435),
new (-6069242, 12874407439, 184192221),
new (-5243292, 12288174930, 194598196),
new (-4541693, 11727464764, 204811866),
new (-3929739, 11191115001, 214623145),
new (-3394638, 10692127188, 223981515),
new (-858728, 7891120334, 238100961),
new (1694314, 4774559593, 178804149),
new (3305370, 2705023953, 90695895),
new (4262235, 1480447133, 79503413),
new (5096475, 389835193, 51726767),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5148860, -289095585, -36850049),
new (4399267, -1265436214, -90852742),
new (3489714, -2439683756, -113409871),
new (2064900, -4271021287, -209923146),
new (-247158, -7115189863, -317028859),
new (-2775525, -9964317235, -331924369),
new (-3347410, -10524883932, -318293149),
new (-4000533, -11126793498, -305355458),
new (-4758683, -11771109846, -293807526),
new (-5655948, -12458367099, -279766373),
new (-6738273, -13164957202, -267721309),
new (-8115816, -13782340730, -266793073),
new (-10177560, -13903794864, -300633815),
new (-13344844, -13003209807, -285831410),
new (-17535536, -10972246630, -267053903),
new (-23237816, -6365569033, -197357909),
new (-26556445, -2611192407, -79667456),
new (-27689200, -11048096, -4734898),
new (-26548299, 2590366033, 70783809),
new (-23226789, 6352639165, 190546405),
new (-17521037, 10966163813, 263015882),
new (-13326724, 13004302164, 284476793),
new (-10154582, 13910218982, 301672249),
new (-8091098, 13772709046, 265362875),
new (-6718140, 13147238327, 269024686),
new (-5633503, 12439135174, 281418633),
new (-4733941, 11750378537, 295801076),
new (-3973512, 11104576553, 307682722),
new (-3318163, 10501233908, 320955439),
new (-2744120, 9939300451, 334926167),
new (-218005, 7097425019, 322487257),
new (2091688, 4260539018, 217466917),
new (3514598, 2436183414, 122965148),
new (4413007, 1278131165, 99963719),
new (5148860, 319638919, 49940049),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5203932, -215226181, -34752141),
new (4581801, -1020545115, -115623374),
new (3742073, -2114485156, -151436261),
new (2444913, -3778772365, -246584823),
new (428641, -6256059759, -398510649),
new (-1973502, -8998442709, -461051482),
new (-2579609, -9618042333, -448851402),
new (-3277219, -10293164093, -435317237),
new (-4091324, -11025117060, -420305638),
new (-5056684, -11808006097, -405057558),
new (-6228901, -12624873032, -389022377),
new (-7788651, -13320467498, -398189156),
new (-10074942, -13433813443, -439398918),
new (-13385936, -12595423025, -415463625),
new (-17814710, -10545733428, -380866628),
new (-23556389, -5955835150, -258239611),
new (-26621392, -2436101515, -104029312),
new (-27689200, -11048096, -4734898),
new (-26612879, 2415277650, 95147671),
new (-23544612, 5942915190, 251435591),
new (-17799239, 10539683223, 376855483),
new (-13366625, 12596568329, 414119490),
new (-10051423, 13441208162, 440634086),
new (-7762367, 13310933118, 396743953),
new (-6205073, 12609837019, 390353258),
new (-5033359, 11788420735, 407537697),
new (-4065633, 11004014248, 423202226),
new (-3249185, 10270561552, 438620146),
new (-2548313, 9592984407, 452806347),
new (-1939769, 8971813358, 465504827),
new (459189, 6237615962, 405141863),
new (2472882, 3767769643, 255226966),
new (3768112, 2110425464, 161970405),
new (4595522, 1033220792, 124731071),
new (5204200, 245484233, 47777101),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5214609, -200571202, -32192986),
new (4702872, -857919353, -119461786),
new (4025834, -1736892750, -189506106),
new (2885770, -3207723123, -294972521),
new (1109068, -5381527045, -466771605),
new (-1020628, -7801886072, -604107985),
new (-1618032, -8425232795, -604041328),
new (-2331940, -9139686772, -599630636),
new (-3185430, -9942822240, -591135828),
new (-4224356, -10833807368, -578847216),
new (-5548134, -11730489190, -576212305),
new (-7350609, -12426935023, -609212676),
new (-9866899, -12656420412, -641617973),
new (-13436386, -11962144363, -606124961),
new (-18175710, -9802303207, -558655114),
new (-23902780, -5428709194, -337842291),
new (-26693450, -2215014702, -134820783),
new (-27689200, -11048096, -4734898),
new (-26684454, 2194195229, 125941010),
new (-23890008, 5415806639, 331045023),
new (-18158952, 9796307765, 554673378),
new (-13415553, 11963408747, 604779213),
new (-9842304, 12664722993, 643012544),
new (-7322280, 12417550463, 607732684),
new (-5516101, 11721899084, 576098275),
new (-4199912, 10813788083, 582398825),
new (-3157614, 9920309199, 595434204),
new (-2301449, 9115440271, 604533329),
new (-1584890, 8399276596, 609553779),
new (-984893, 7774279960, 610243891),
new (1141465, 5362183687, 474956360),
new (2915305, 3196029753, 305069356),
new (4041052, 1744221521, 197499842),
new (4716568, 870569372, 128565106),
new (5216138, 229486840, 44911897),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5230579, -178653480, -28365370),
new (4842057, -670828582, -115155719),
new (4265238, -1415329529, -202051588),
new (3412368, -2508413322, -352646771),
new (1894713, -4354994197, -551682438),
new (16834, -6449656890, -744605174),
new (-561521, -7047767063, -756926671),
new (-1238195, -7709885434, -775737831),
new (-2057401, -8468613746, -796345203),
new (-3139207, -9274780034, -833605021),
new (-4668178, -10047805020, -892993473),
new (-6671865, -10830000759, -951678345),
new (-9490525, -11187859425, -985867066),
new (-13454832, -10620307222, -940175872),
new (-18641581, -8522310759, -832700453),
new (-24246732, -4653608379, -459511144),
new (-26769386, -1904082546, -177999494),
new (-27689200, -11048096, -4734898),
new (-26759668, 1883271875, 169120859),
new (-24232471, 4640740782, 452717366),
new (-18622959, 8516485412, 828729991),
new (-13432242, 10621295333, 938953290),
new (-9463308, 11196459649, 987226289),
new (-6640543, 10820884499, 950114786),
new (-4631911, 10038648551, 892984001),
new (-3098613, 9266269684, 835035151),
new (-2027288, 8444989801, 802596506),
new (-1205181, 7684417954, 782782615),
new (-525621, 7020469786, 764768081),
new (55563, 6420589788, 753256945),
new (1912256, 4352539412, 557650180),
new (3428098, 2511399262, 359756673),
new (4280414, 1422615373, 210037979),
new (4855715, 683439966, 124252430),
new (5233990, 205565295, 40627441),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5259386, -139120057, -21460853),
new (4902525, -588752085, -107388136),
new (4544505, -1039939460, -193315420),
new (3759996, -2033723168, -340118128),
new (2497927, -3485033084, -590884481),
new (901717, -5078036270, -885979594),
new (406888, -5404693811, -966235510),
new (-288115, -5715617133, -1060468574),
new (-1306756, -6155542669, -1156504925),
new (-2542050, -6660004744, -1265322973),
new (-4074485, -7220433492, -1393542851),
new (-6232435, -7523692940, -1544578862),
new (-9172415, -7629022309, -1685052123),
new (-13443156, -7186039961, -1630786268),
new (-18802042, -5991807873, -1314026232),
new (-24425181, -3380641911, -668248019),
new (-26802558, -1385718374, -251964988),
new (-27689200, -11048096, -4734898),
new (-26791570, 1364953632, 243076422),
new (-24408325, 3367957586, 661412623),
new (-18780086, 5986379838, 1310011113),
new (-13416517, 7187661188, 1629509598),
new (-9140332, 7636755372, 1686716042),
new (-6197297, 7514839268, 1543853414),
new (-4031500, 7211310088, 1393609320),
new (-2493916, 6651533984, 1266843854),
new (-1253474, 6147724554, 1159480218),
new (-250554, 5729731607, 1059200114),
new (447755, 5420449409, 965030982),
new (925338, 5073797172, 889677686),
new (2515414, 3482456982, 596829629),
new (3775486, 2036797381, 347252835),
new (4559607, 1047146997, 201288350),
new (4916117, 601292993, 116472732),
new (5266181, 162429250, 32901582),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5281159, 27374944, -16533830),
new (4584737, 26638010, -185021736),
new (3888316, 25901076, -353509642),
new (1558615, 24206895, -942165960),
new (-667631, 24102951, -1504522029),
new (-2912195, 23864125, -1954315730),
new (-3549341, 22088381, -2068988179),
new (-4163495, 20312636, -2168275958),
new (-4901218, 18536891, -2252179068),
new (-5548573, 16111192, -2320541674),
new (-7141290, 3882006, -2460698317),
new (-8520311, 2355518, -2568879380),
new (-10096354, 1789459, -2609488337),
new (-12989304, -1341700, -2477423809),
new (-16788865, -4472859, -2183489845),
new (-22403981, -7524914, -1347323080),
new (-26337792, -7372751, -488339657),
new (-27689200, -11048096, -4734898),
new (-26323685, -13285717, 479402763),
new (-22390504, -13940760, 1343422863),
new (-16812726, -13445928, 2179353997),
new (-13027989, -12600520, 2476954001),
new (-10188291, -11755111, 2611439524),
new (-8577734, -1949865, 2573067232),
new (-7213119, -1578764, 2463306954),
new (-5552960, -1205701, 2315877672),
new (-4915641, -832638, 2248615294),
new (-4190762, -459576, 2166342766),
new (-3560769, -86513, 2069060087),
new (-2937408, 286534, 1956767519),
new (-631403, 3252064, 1499644667),
new (1577516, 6119114, 943852675),
new (3925351, 9104422, 352762429),
new (4618069, 12120980, 187458120),
new (5310788, 15137537, 22153810),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5256361, 172668204, -22266476),
new (4899123, 620425990, -109044411),
new (4540724, 1069739123, -195822345),
new (3756105, 2061397645, -343399630),
new (2493534, 3511073269, -595011026),
new (900842, 5106598560, -889970770),
new (417099, 5442883350, -967248361),
new (-278730, 5750750511, -1061394719),
new (-1292493, 6180853002, -1158687477),
new (-2529165, 6680962896, -1267289048),
new (-4062979, 7237039463, -1395292447),
new (-6220196, 7532551317, -1545574163),
new (-9158216, 7637118367, -1686627936),
new (-13431251, 7187330972, -1631953909),
new (-18792228, 5985974837, -1314846045),
new (-24417624, 3367586465, -668693676),
new (-26799361, 1368281467, -252537676),
new (-27689200, -11048096, -4734898),
new (-26794802, -1388904382, 243646569),
new (-24412944, -3390441112, 662098633),
new (-18785845, -6004766508, 1310945287),
new (-13423418, -7201840629, 1630711197),
new (-9148897, -7647335287, 1688046726),
new (-6213899, -7524325234, 1545086321),
new (-4050683, -7218392872, 1394399443),
new (-2515397, -6656504355, 1267124386),
new (-1277253, -6150582510, 1159251158),
new (-263067, -5717215954, 1062145884),
new (434142, -5405983883, 968235705),
new (918012, -5065517506, 891402317),
new (2508872, -3470433621, 598369869),
new (3769690, -2021165184, 348617306),
new (4553946, -1028395595, 202620988),
new (4911023, -578962565, 117672106),
new (5266181, -131885917, 32901582),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5230974, 208709581, -28291316),
new (4842502, 698573935, -115822355),
new (4265733, 1440764134, -203458915),
new (3412876, 2531595975, -354796809),
new (1895287, 4375962547, -554531423),
new (27662, 6456155721, -750574744),
new (-551501, 7053435914, -762822623),
new (-1228984, 7714725128, -781560393),
new (-2049000, 8472624284, -802094373),
new (-3121354, 9287568933, -836351537),
new (-4652228, 10057127760, -895441099),
new (-6658143, 10836273551, -953750274),
new (-9474037, 11195081819, -988804841),
new (-13441232, 10620127154, -942655488),
new (-18630355, 8515333578, -834596482),
new (-24238098, 4639699012, -460783906),
new (-26764934, 1885543988, -178802978),
new (-27689200, -11048096, -4734898),
new (-26764159, -1906206629, 169917643),
new (-24238848, -4660576261, 453681099),
new (-18631095, -8531463796, 829967652),
new (-13442056, -10631417177, 940494769),
new (-9475146, -11201255378, 989174193),
new (-6659377, -10828042963, 953321621),
new (-4653839, -10044059623, 895827869),
new (-3123157, -9269377276, 837616434),
new (-2046545, -8454559032, 803054836),
new (-1226293, -7694471503, 782799185),
new (-548574, -7030993915, 764339745),
new (30801, -6431551829, 752376034),
new (1903212, -4343016283, 559779363),
new (3419896, -2498176856, 361687500),
new (4272579, -1406037442, 211882513),
new (4848663, -663065663, 125912510),
new (5233990, -175021962, 40627441),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5216138, 229486840, -31821897),
new (4705452, 883485535, -119579398),
new (4028700, 1759906055, -190303406),
new (2898655, 3217446209, -298838560),
new (1123237, 5385536265, -471468830),
new (-1005158, 7800088699, -609656178),
new (-1603707, 8423216646, -609545232),
new (-2318770, 9137462389, -605092872),
new (-3173415, 9940389622, -596556394),
new (-4214004, 10831752341, -584106690),
new (-5530075, 11736262066, -578962585),
new (-7334621, 12429652268, -611647335),
new (-9849168, 12662757136, -645427022),
new (-13421366, 11961568166, -609237019),
new (-18163668, 9794641838, -561150247),
new (-23893553, 5414326963, -339571439),
new (-26688296, 2195859574, -135751842),
new (-27689200, -11048096, -4734898),
new (-26689639, -2216557257, 126866845),
new (-23897326, -5434145700, 332172121),
new (-18168392, -9809424498, 556083746),
new (-13427323, -11970899560, 606601899),
new (-9856172, -12666688231, 645208585),
new (-7342654, -12424010759, 611874241),
new (-5539121, -11725833590, 580162143),
new (-4217306, -10824662852, 584453530),
new (-3177530, -9932609199, 597019949),
new (-2323279, -9128486268, 605786489),
new (-1608612, -8413045078, 610468967),
new (-1010476, -7788739738, 610813736),
new (1118199, -5369636659, 474938646),
new (2893920, -3197021901, 304566985),
new (4032010, -1728849461, 199628425),
new (4708430, -851280351, 130480832),
new (5216138, -198943506, 44911897),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5204200, 245484233, -34687101),
new (4585808, 1044653771, -115373745),
new (3756375, 2125427496, -154144124),
new (2460308, 3783919638, -250242438),
new (445610, 6255014480, -402964698),
new (-1954932, 8991101699, -466319114),
new (-2562403, 9610885395, -454093092),
new (-3262099, 10286983830, -440373629),
new (-4077426, 11019028567, -425380025),
new (-5044028, 11802020071, -410145320),
new (-6214617, 12622014351, -393396211),
new (-7771123, 13320758623, -400871731),
new (-10056361, 13439524162, -443795858),
new (-13370198, 12594325884, -419072192),
new (-17802141, 10537631140, -383755619),
new (-23546760, 5941132877, -260274413),
new (-26615765, 2416530792, -105045860),
new (-27689200, -11048096, -4734898),
new (-26618532, -2437256218, 96160127),
new (-23552559, -5960255409, 252671576),
new (-17809511, -10551529075, 378390759),
new (-13379530, -12602445724, 416094505),
new (-10067356, -13442012155, 443170194),
new (-7783764, -13316933223, 401520180),
new (-6223358, -12619510762, 393736931),
new (-5051348, -11801085537, 410203774),
new (-4085426, -11017475373, 425657494),
new (-3270760, -10284804639, 440872137),
new (-2572549, -9608922280, 454619966),
new (-1965900, -8988611440, 467044610),
new (435448, -6247196677, 406012242),
new (2450960, -3770816145, 255562288),
new (3747942, -2106855761, 161786678),
new (4586658, -1014657746, 126817819),
new (5204200, -214940900, 47777101),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5148860, 319638919, -36850049),
new (4404352, 1288444465, -90325837),
new (3505822, 2447399842, -115953137),
new (2082189, 4272715906, -213423892),
new (-228078, 7110330906, -321299893),
new (-2755508, 9953756061, -336771038),
new (-3328745, 10514674678, -323169388),
new (-3983267, 11116976291, -310264143),
new (-4742834, 11761698695, -298745055),
new (-5641536, 12449373454, -284728529),
new (-6725315, 13156392513, -272703875),
new (-8097113, 13780774575, -269665650),
new (-10158328, 13909014990, -305477347),
new (-13328586, 13001737151, -289810436),
new (-17522568, 10963810695, -270239367),
new (-23227881, 6350623118, -199623672),
new (-26550460, 2591306211, -80748629),
new (-27689200, -11048096, -4734898),
new (-26554305, -2612054907, 71861821),
new (-23235212, -6369226438, 191864385),
new (-17531932, -10977051793, 264645642),
new (-13340456, -13008930285, 286575973),
new (-10172362, -13910443817, 304536709),
new (-8113263, -13778365333, 270623325),
new (-6734687, -13160301275, 272334452),
new (-5651933, -12453159204, 284553770),
new (-4754238, -11765350970, 298767608),
new (-3995658, -11120484629, 310486654),
new (-3342105, -10518027494, 323594210),
new (-2769813, -9956939572, 337399274),
new (-242104, -7108611627, 324027949),
new (2069361, -4265136887, 218434706),
new (3494102, -2433979558, 123346487),
new (4403595, -1260116262, 102179605),
new (5148860, -289095585, 49940049),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5096475, 389835193, -38636767),
new (4254454, 1489835500, -69633762),
new (3299041, 2713113128, -84356534),
new (1687354, 4783456126, -175539313),
new (-865905, 7900316415, -237731619),
new (-3402928, 10702648517, -226637802),
new (-3937437, 11200891085, -217595428),
new (-4548787, 11736479660, -208098853),
new (-5249748, 12296391729, -198193275),
new (-6075062, 12881826405, -188097216),
new (-7078739, 13490700921, -178703300),
new (-8351157, 14072406726, -172797904),
new (-10216251, 14241888716, -201493956),
new (-13249618, 13243327219, -200535008),
new (-17291956, 11250077423, -187396995),
new (-22928670, 6701191854, -148435640),
new (-26489735, 2741170169, -59876783),
new (-27689200, -11048096, -4734898),
new (-26494473, -2761939617, 50988745),
new (-22933242, -6714775542, 141584167),
new (-17303346, -11262367278, 181706313),
new (-13263578, -13249742554, 197090525),
new (-10232782, -14242429530, 200295671),
new (-8370227, -14071196731, 174005155),
new (-7090409, -13498144895, 178151566),
new (-6088032, -12889559424, 187719725),
new (-5263999, -12304401785, 197992961),
new (-4564302, -11744754746, 208078653),
new (-3954194, -11209414696, 217757161),
new (-3420900, -10711399631, 226981454),
new (-883123, -7903633278, 240195297),
new (1671653, -4780439327, 180294453),
new (3284604, -2703948509, 91544026),
new (4245975, -1469692243, 80274073),
new (5096475, -359291860, 51726767),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (4982292, 542840084, -24003259),
new (4094845, 1695980325, -45040008),
new (3107520, 2959778755, -57560137),
new (1243013, 5356026510, -125020102),
new (-1456723, 8613182714, -155351704),
new (-3940169, 11293926527, -134495338),
new (-4423025, 11718021785, -127972489),
new (-4981408, 12177674270, -121329217),
new (-5637360, 12674112878, -114290817),
new (-6418229, 13198976552, -108388999),
new (-7347777, 13719033761, -107276245),
new (-8521137, 14230801823, -105767506),
new (-10222688, 14451627787, -124255586),
new (-13171713, 13430378569, -127987314),
new (-17099834, 11475727408, -118324709),
new (-22638229, 7025498757, -101273026),
new (-26430603, 2878704616, -40665739),
new (-27689200, -11048096, -4734898),
new (-26436138, -2899493694, 31776345),
new (-22643554, -7037900263, 94476229),
new (-17108754, -11483193926, 113453241),
new (-13187536, -13436100923, 124355789),
new (-10241420, -14451355617, 122833018),
new (-8535065, -14238848650, 105330085),
new (-7361508, -13729651689, 106556203),
new (-6433509, -13210256577, 107823534),
new (-5654171, -12686042402, 113882949),
new (-4999734, -12190240695, 121081966),
new (-4442840, -11731203548, 127886679),
new (-3961444, -11307698184, 134570084),
new (-1475745, -8619851274, 157835880),
new (1224622, -5357207806, 129581557),
new (3090668, -2954399039, 64564551),
new (4079678, -1683884539, 54452855),
new (4982292, -512296750, 37093259),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (4868452, 695386620, -8805911),
new (3939023, 1896104777, -20680985),
new (2911762, 3212889758, -32924064),
new (761212, 5967475436, -66295534),
new (-2006650, 9258249111, -76663320),
new (-4352625, 11714266555, -56619989),
new (-4792082, 12077519684, -55887692),
new (-5307418, 12475714320, -55548933),
new (-5916268, 12907432196, -54905280),
new (-6649814, 13370847457, -53900016),
new (-7546068, 13852804830, -53824023),
new (-8674230, 14325417261, -53092525),
new (-10254090, 14593906884, -56689574),
new (-13098193, 13594452469, -62926077),
new (-16913029, 11658479773, -59590164),
new (-22344498, 7347800600, -54215133),
new (-26365997, 3007669770, -21835551),
new (-27689200, -11048096, -4734898),
new (-26372289, -3028478515, 12944638),
new (-22350539, -7359080336, 47469491),
new (-16921257, -11662677534, 55184701),
new (-13114380, -13598018069, 59458204),
new (-10274914, -14592861855, 55053883),
new (-8688012, -14337905721, 52106156),
new (-7561702, -13866384293, 52956549),
new (-6667296, -13385514652, 53152187),
new (-5935534, -12923121197, 54292202),
new (-5328440, -12492400381, 55076357),
new (-4814832, -12095175840, 55555036),
new (-4377072, -11732864919, 56425655),
new (-2028497, -9269221104, 78908994),
new (740265, -5972644208, 70673486),
new (2892615, -3211103608, 39754662),
new (3921791, -1887243201, 29937398),
new (4868452, -664843287, 21895911),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
new (5376000, 15271667, 6545000),
            };
    }
}
