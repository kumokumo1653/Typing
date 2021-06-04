using System.Collections;
using System.Collections.Generic;
using System;

public class Question {

    public string q {get;}
    public string kana {get;}
    public Question(string q, string kana){
        this.q = String.Copy(q);
        this.kana = String.Copy(kana);
    }

    static private Dictionary<string, string[]> wordTable = new Dictionary<string, string[]>(){
        {"あ", new string[]{"a"}},
        {"い", new string[]{"i"}},
        {"う", new string[]{"u"}},//"wu"削除　リファクタリングのときに追加するかも
        {"え", new string[]{"e"}},
        {"お", new string[]{"o"}},
        {"か", new string[]{"ka", "ca"}},
        {"き", new string[]{"ki"}},
        {"く", new string[]{"ku", "qu", "cu"}},
        {"け", new string[]{"ke"}},
        {"こ", new string[]{"ko", "co"}},
        {"さ", new string[]{"sa"}},
        {"し", new string[]{"si", "ci", "shi"}},
        {"す", new string[]{"su"}},
        {"せ", new string[]{"se", "ce"}},
        {"そ", new string[]{"so"}},
        {"た", new string[]{"ta"}},
        {"ち", new string[]{"ti", "chi"}},
        {"つ", new string[]{"tu", "tsu"}},
        {"て", new string[]{"te"}},
        {"と", new string[]{"to"}},
        {"な", new string[]{"na"}},
        {"に", new string[]{"ni"}},
        {"ぬ", new string[]{"nu"}},
        {"ね", new string[]{"ne"}},
        {"の", new string[]{"no"}},
        {"は", new string[]{"ha"}},
        {"ひ", new string[]{"hi"}},
        {"ふ", new string[]{"hu", "fu"}},
        {"へ", new string[]{"he"}},
        {"ほ", new string[]{"ho"}},
        {"ま", new string[]{"ma"}},
        {"み", new string[]{"mi"}},
        {"む", new string[]{"mu"}},
        {"め", new string[]{"me"}},
        {"も", new string[]{"mo"}},
        {"や", new string[]{"ya"}},
        {"ゆ", new string[]{"yu"}},
        {"よ", new string[]{"yo"}},
        {"ら", new string[]{"ra"}},
        {"り", new string[]{"ri"}},
        {"る", new string[]{"ru"}},
        {"れ", new string[]{"re"}},
        {"ろ", new string[]{"ro"}},
        {"わ", new string[]{"wa"}},
        {"を", new string[]{"wo"}},
        {"ん", new string[]{"n","nn","xn"}},
        {"が", new string[]{"ga"}},
        {"ぎ", new string[]{"gi"}},
        {"ぐ", new string[]{"gu"}},
        {"げ", new string[]{"ge"}},
        {"ご", new string[]{"go"}},
        {"ざ", new string[]{"za"}},
        {"じ", new string[]{"zi", "ji"}},
        {"ず", new string[]{"zu"}},
        {"ぜ", new string[]{"ze"}},
        {"ぞ", new string[]{"zo"}},
        {"だ", new string[]{"da"}},
        {"ぢ", new string[]{"di"}},
        {"づ", new string[]{"du"}},
        {"で", new string[]{"de"}},
        {"ど", new string[]{"do"}},
        {"ば", new string[]{"ba"}},
        {"び", new string[]{"bi"}},
        {"ぶ", new string[]{"bu"}},
        {"べ", new string[]{"be"}},
        {"ぼ", new string[]{"bo"}},
        {"ぱ", new string[]{"pa"}},
        {"ぴ", new string[]{"pi"}},
        {"ぷ", new string[]{"pu"}},
        {"ぺ", new string[]{"pe"}},
        {"ぽ", new string[]{"po"}},
        {"ぁ", new string[]{"la", "xa"}},
        {"ぃ", new string[]{"li", "xi"}},
        {"ぅ", new string[]{"lu", "xu"}},
        {"ぇ", new string[]{"le", "xe"}},
        {"ぉ", new string[]{"lo", "xo"}},
        {"ゎ", new string[]{"lwa", "xwa"}},
        {"っ", new string[]{"ltu", "xtu", "ltsu", "xtsu"}},
        {"ゃ", new string[]{"lya", "xya"}},
        {"ゅ", new string[]{"lyu", "xya"}},
        {"ょ", new string[]{"lyo", "xyo"}},
        {"きゃ", new string[]{"kya", "kilya", "kixya"}},
        {"きぃ", new string[]{"kyi", "kili", "kixi"}},
        {"きゅ", new string[]{"kyu", "kilyu", "kixyu"}},
        {"きぇ", new string[]{"kye", "kile", "kixe"}},
        {"きょ", new string[]{"kyo", "kilyo", "kixyo"}},
        {"くぁ", new string[]{"kwa", "qa", "kula", "kuxa", "qula", "quxa", "cula", "cuxa"}},
        {"くぃ", new string[]{"kwi", "qyi", "qi", "kuli", "kuxi", "quli", "quxi", "culi", "cuxi"}},
        {"くぅ", new string[]{"kwu", "qwu", "kulu", "kuxu", "qulu", "quxu", "culu", "cuxu"}},
        {"くぇ", new string[]{"kwe", "qye", "qe", "kule", "kuxe", "qule", "quxe", "cule", "cuxe"}},
        {"くぉ", new string[]{"kwo", "qo", "kulo", "kuxo", "qulo", "quxo", "culo", "cuxo"}},
        {"くゃ", new string[]{"qya", "kulya", "kuxya", "qulya", "quxya", "culya", "cuxya"}},
        {"くゅ", new string[]{"qyu", "kulyu", "kuxyu", "qulyu", "quxyu", "culyu", "cuxyu"}},
        {"くょ", new string[]{"qyo", "kulyo", "kuxyo", "qulyo", "quxyo", "culyo", "cuxyo"}},
        {"ぎゃ", new string[]{"gya", "gilya", "gixya"}},
        {"ぎぃ", new string[]{"gyi", "gili", "gixi"}},
        {"ぎゅ", new string[]{"gyu", "gilyu", "gixyu"}},
        {"ぎぇ", new string[]{"gye", "gile", "gixe"}},
        {"ぎょ", new string[]{"gyo", "gilyo", "gixyo"}},
        {"ぐぁ", new string[]{"gwa", "gula", "guxa"}},
        {"ぐぃ", new string[]{"gwi", "guli", "guxi"}},
        {"ぐぅ", new string[]{"gwu", "gulu", "guxu"}},
        {"ぐぇ", new string[]{"gwe", "gule", "guxe"}},
        {"ぐぉ", new string[]{"gwo", "gulo", "guxo"}},
        {"しゃ", new string[]{"sya", "cilya", "cixya", "silya", "sixya", "sha"}},
        {"しぃ", new string[]{"syi", "cili", "cixi", "sili", "sixi"}},
        {"しゅ", new string[]{"syu", "cilyu", "cixyu", "silyu", "sixyu", "shu"}},
        {"しぇ", new string[]{"sye", "cile", "cixe", "sile", "sixe", "she"}},
        {"しょ", new string[]{"syo", "cilyo", "cixyo", "silyo", "sixyo", "sho"}},
        {"すぁ", new string[]{"swa", "sula", "suxa"}},
        {"すぃ", new string[]{"swi", "suli", "suxi"}},
        {"すぅ", new string[]{"swu", "sulu", "suxu"}},
        {"すぇ", new string[]{"swe", "sule", "suxe"}},
        {"すぉ", new string[]{"swo", "sulo", "suxo"}},
        {"じゃ", new string[]{"zya", "jya", "jilya", "jixya", "zilya", "zixya", "ja"}},
        {"じぃ", new string[]{"zyi", "jyi", "jili", "jixi", "zili", "zixi"}},
        {"じゅ", new string[]{"zyu", "jyu", "jilyu", "jixyu", "zilyu", "zixyu", "ju"}},
        {"じぇ", new string[]{"zye", "jye", "jile", "jixe", "zile", "zixe", "je"}},
        {"じょ", new string[]{"zyo", "jyo", "jilyo", "jixyo", "zilyo", "zixyo", "jo"}},
        {"ちゃ", new string[]{"tya", "cya", "chilya", "chixya", "tilya", "tixya", "cha"}},
        {"ちぃ", new string[]{"tyi", "cyi", "chili", "chixi", "tili", "tixi"}},
        {"ちゅ", new string[]{"tyu", "cyu", "chilyu", "chixyu", "tilyu", "tixyu", "chu"}},
        {"ちぇ", new string[]{"tye", "cye", "chile", "chixi", "tile", "tixe", "che"}},
        {"ちょ", new string[]{"tyo", "cyo", "chilyo", "chixyo", "tilyo", "tixyo", "cho"}},
        {"つぁ", new string[]{"tsa", "sula", "suxa", "tsula", "tsuxa"}},
        {"つぃ", new string[]{"tsi", "suli", "suxi", "tsuli", "tsuxi"}},
        {"つぅ", new string[]{"sulu", "suxu", "tsulu", "tsuxu"}},
        {"つぇ", new string[]{"tse", "sule", "suxe", "tsule", "tsuxe"}},
        {"つぉ", new string[]{"tso", "sulo", "suxo", "tsulo", "tsuxo"}},
        {"てゃ", new string[]{"tha", "telya", "texya"}},
        {"てぃ", new string[]{"thi", "teli", "texi"}},
        {"てゅ", new string[]{"thu", "telyu", "texyu"}},
        {"てぇ", new string[]{"the", "tele", "texe"}},
        {"てょ", new string[]{"tho", "telyo", "texyo"}},
        {"とぁ", new string[]{"twa", "tola", "toxa"}},
        {"とぃ", new string[]{"twi", "toli", "toxi"}},
        {"とぅ", new string[]{"twu", "tolu", "toxu"}},
        {"とぇ", new string[]{"twe", "tole", "toxe"}},
        {"とぉ", new string[]{"two", "tolo", "toxo"}},
        {"ぢゃ", new string[]{"dya", "dilya", "dixya"}},
        {"ぢぃ", new string[]{"dyi", "dili", "dixi"}},
        {"ぢゅ", new string[]{"dyu", "dilyu", "dixyu"}},
        {"ぢぇ", new string[]{"dye", "dile", "dixe"}},
        {"ぢょ", new string[]{"dyo", "dilyo", "dixyo"}},
        {"でゃ", new string[]{"dha", "delya", "dexya"}},
        {"でぃ", new string[]{"dhi", "deli", "dexi"}},
        {"でゅ", new string[]{"dhu", "delyu", "dexyu"}},
        {"でぇ", new string[]{"dhe", "dele", "dexe"}},
        {"でょ", new string[]{"dho", "delyo", "dexyo"}},
        {"どぁ", new string[]{"dwa", "dola", "doxa"}},
        {"どぃ", new string[]{"dwi", "doli", "doxi"}},
        {"どぅ", new string[]{"dwu", "dolu", "doxu"}},
        {"どぇ", new string[]{"dwe", "dole", "doxe"}},
        {"どぉ", new string[]{"dwo", "dolo", "doxo"}},
        {"にゃ", new string[]{"nya", "nilya", "nixya"}},
        {"にぃ", new string[]{"nyi", "nili", "nixi"}},
        {"にゅ", new string[]{"nyu", "nilyu", "nixyu"}},
        {"にぇ", new string[]{"nye", "nile", "nixe"}},
        {"にょ", new string[]{"nyo", "nilyo", "nixyo"}},
        {"ひゃ", new string[]{"hya", "hilya", "hixya"}},
        {"ひぃ", new string[]{"hyi", "hili", "hixi"}},
        {"ひゅ", new string[]{"hyu", "hilyu", "hixyu"}},
        {"ひぇ", new string[]{"hye", "hile", "hixe"}},
        {"ひょ", new string[]{"hyo", "hilyo", "hixyo"}},
        {"ふぁ", new string[]{"fwa", "fula", "fuxa", "hula", "huxa", "hwa", "fa"}},
        {"ふぃ", new string[]{"fwi", "fuli", "fuxi", "huli", "huxi", "hwi", "fi", "fyi"}},
        {"ふぅ", new string[]{"fwu", "fulu", "fuxu", "hulu", "huxu", "hwu",}},
        {"ふぇ", new string[]{"fwe", "fule", "fuxe", "hule", "huxe", "hwe", "fe", "fye"}},
        {"ふぉ", new string[]{"fwo", "fulo", "fuxo", "hulo", "huxo", "hwo", "fo"}},
        {"ふゃ", new string[]{"fya", "fulya", "fuxya", "hulya", "huxya"}},
        {"ふゅ", new string[]{"fyu", "fulyu", "fuxyu", "hulyu", "huxyu"}},
        {"ふょ", new string[]{"fyo", "fulyo", "fuxyo", "hulyo", "huxyo"}},
        {"びゃ", new string[]{"bya", "bilya", "bixya"}},
        {"びぃ", new string[]{"byi", "bili", "bixi"}},
        {"びゅ", new string[]{"byu", "bilyu", "bixyu"}},
        {"びぇ", new string[]{"bye", "bile", "bixe"}},
        {"びょ", new string[]{"byo", "bilyo", "bixyo"}},
        {"ぴゃ", new string[]{"pya", "pilya", "pixya"}},
        {"ぴぃ", new string[]{"pyi", "pili", "pixi"}},
        {"ぴゅ", new string[]{"pyu", "pilyu", "pixyu"}},
        {"ぴぇ", new string[]{"pye", "pile", "pixe"}},
        {"ぴょ", new string[]{"pyo", "pilyo", "pixyo"}},
        {"みゃ", new string[]{"mya", "milya", "mixya"}},
        {"みぃ", new string[]{"myi", "mili", "mixi"}},
        {"みゅ", new string[]{"myu", "milyu", "mixyu"}},
        {"みぇ", new string[]{"mye", "mile", "mixe"}},
        {"みょ", new string[]{"myo", "milyo", "mixyo"}},
        {"りゃ", new string[]{"rya", "rilya", "rixya"}},
        {"りぃ", new string[]{"ryi", "rili", "rixi"}},
        {"りゅ", new string[]{"ryu", "rilyu", "rixyu"}},
        {"りぇ", new string[]{"rye", "rile", "rixe"}},
        {"りょ", new string[]{"ryo", "rilyo", "rixyo"}},
        {"うぁ", new string[]{"wha", "ula", "uxa"}},
        {"うぃ", new string[]{"whi", "uli", "uxi"}},
        {"うぇ", new string[]{"whe", "ule", "uxe"}},
        {"うぉ", new string[]{"who", "ulo", "uxo"}},
        {"ゔぁ", new string[]{"va", "vula", "vuxa"}},
        {"ゔぃ", new string[]{"vi", "vuli", "vuxi", "vyi"}},
        {"ゔ", new string[]{"vu"}},
        {"ゔぇ", new string[]{"ve", "vule", "vuxe", "vye"}},
        {"ゔぉ", new string[]{"vo", "vulo", "vuxo"}},
        {"ゔゃ", new string[]{"vya", "vulya", "vuxya"}},
        {"ゔゅ", new string[]{"vyu", "vulyu", "vuxyu"}},
        {"ゔょ", new string[]{"vyo", "vulyo", "vuxyo"}},

    };


    public void TransformWords(out string[][] str){
        str = new string[kana.Length][]; 
        int skip = 0;
        int tsuCount = 0;
        for (int i = 0; i < kana.Length; i++){
            //ひらがなであれば
            if(kana[i] >= '\u3040' && kana[i] <= '\u309F'){
                string s = new string(kana[i], 1);
                str[i - skip] = new string[wordTable[s].Length];
                Array.Copy(wordTable[s], str[i - skip], wordTable[s].Length);

                //んの指定
                if(i != 0 && kana[i - 1] == 'ん'){
                    if(kana[i] == 'あ' || kana[i] == 'い' || kana[i] == 'う' || kana[i] == 'え' || kana[i] == 'お' ||
                    kana[i] == 'な' || kana[i] == 'に' || kana[i] == 'ぬ' || kana[i] == 'ね' || kana[i] == 'の' ||
                    kana[i] == 'や' || kana[i] == 'ゆ' || kana[i] == 'よ' || kana[i] == 'ん'){
                            //二文字のみ許可
                            str[i - skip - 1] = new string[]{"nn", "xn"};
                    }
                }
                //小文字の設定
                if(kana[i] == 'ゃ' || kana[i] == 'ゅ' || kana[i] == 'ょ' || kana[i] == 'ぁ' || kana[i] == 'ぃ' || kana[i] == 'ぅ' || kana[i] == 'ぇ' || kana[i] == 'ぉ'){
                    string t = new string(new char[]{kana[i - 1], kana[i]});
                    string[] val;
                    if(wordTable.TryGetValue(t,out val)){
                        str[i - skip - 1] = new string[wordTable[t].Length];
                        Array.Copy(wordTable[t], str[i - skip - 1], wordTable[t].Length);
                        skip++;
                    }
                }

                //促音の設定
                if(kana[i] == 'っ'){
                    tsuCount++;
                    //次がひらがなじゃない。または末尾
                    if(i + 1 == kana.Length || kana[i + 1] < '\u3040' || kana[i + 1] > '\u309F'){
                        tsuCount--;
                        string[] t = new string[]{"l","x"};
                        Array.Resize(ref t, t.Length + wordTable["っ"].Length);
                        wordTable["っ"].CopyTo(t, t.Length - wordTable["っ"].Length);
                        for(int j = tsuCount; j > 0; j--){
                            str[i - skip - j] = new string[t.Length];
                            Array.Copy(t, str[i - skip - j], t.Length);
                        }
                        tsuCount = 0;
                    }
                }else if(tsuCount != 0){
                    //追加文字の指定
                    string[] t = new string[0];
                    if(!(s == "あ" || s == "い" || s == "う" || s == "え" || s == "お" || s == "ん")){
                        for(int j = 0; j < wordTable[s].Length; j++){
                            bool f = true;
                            for(int k = 0; k < t.Length; k++){
                                if(t[k] == wordTable[s][j]){
                                    f = false;
                                }
                            }
                            if(f){
                                Array.Resize(ref t, t.Length + 1);
                                t[t.Length - 1] = new string(wordTable[s][j][0], 1);
                            }
                        }
                    }
                    
                    Array.Resize(ref t, t.Length + wordTable["っ"].Length);
                    wordTable["っ"].CopyTo(t, t.Length - wordTable["っ"].Length);
                    for(int j = tsuCount; j > 0; j--){
                        str[i - skip - j] = new string[t.Length];
                        Array.Copy(t, str[i - skip - j], t.Length);
                    }
                    tsuCount = 0;
                }
            }else{
                
                str[i - skip] = new string[]{new string(kana[i], 1)};
                switch (kana[i])
                {
                    case 'ー': str[i - skip] = new string[]{"-"};break; 
                    case '、': str[i - skip] = new string[]{","};break; 
                    case '。': str[i - skip] = new string[]{"."};break; 
                    case '！': str[i - skip] = new string[]{"!"};break; 
                    case '？': str[i - skip] = new string[]{"?"};break; 
                    default:break;
                }
            }
        }
        Array.Resize<string[]>(ref str, kana.Length - skip);
    }
    
}
