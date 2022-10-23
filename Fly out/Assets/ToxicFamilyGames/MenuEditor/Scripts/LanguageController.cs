using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToxicFamilyGames.MenuEditor
{
    public class LanguageController : MonoBehaviour
    {
        private static LanguageController instance;

        #region ISO 639-1
        /*
         *ab    Abkhazian	       
         *aa    Afar	           
         *af    Afrikaans	       
         *ak    Akan	           
         *sq    Albanian	       
         *am    Amharic	       
         *ar    Arabic	           
         *an    Aragonese	       
         *hy    Armenian	       
         *as    Assamese	       
         *av    Avaric	           
         *ae    Avestan	       
         *ay    Aymara	           
         *az    Azerbaijani	   
         *bm    Bambara	       
         *ba    Bashkir	       
         *eu    Basque	           
         *be    Belarusian	       
         *bn    Bengali	       
         *bi    Bislama	       
         *bs    Bosnian	       
         *br    Breton	           
         *bg    Bulgarian	       
         *my    Burmese	       
         *ca    Catalan           
         *ch    Chamorro	       
         *ce    Chechen	       
         *ny    Chichewa          
         *zh    Chinese	       
         *cu    Church Slavonik	
         *cv    Chuvash	       
         *kw    Cornish	       
         *co    Corsican	       
         *cr    Cree	           
         *hr    Croatian	       
         *cs    Czech	           
         *da    Danish	           
         *dv    Divehi            
         *nl    Dutch         	   
         *dz    Dzongkha	       
         *en    English	       
         *eo    Esperanto	       
         *et    Estonian	       
         *ee    Ewe	           
         *fo    Faroese	       
         *fj    Fijian	           
         *fi    Finnish	       
         *fr    French	           
         *fy    Western Frisian	
         *ff    Fulah	           
         *gd    Gaelic            
         *gl    Galician	       
         *lg    Ganda	           
         *ka    Georgian	       
         *de    German	           
         *el    Greek             
         *kl    Kalaallisut       
         *gn    Guarani	       
         *gu    Gujarati	       
         *ht    Haitian           
         *ha    Hausa	           
         *he    Hebrew	           
         *hz    Herero	           
         *hi    Hindi	           
         *ho    Hiri Motu	       
         *hu    Hungarian	       
         *is    Icelandic	       
         *io    Ido	           
         *ig    Igbo	           
         *id    Indonesian	       
         *ia    Interlingua	   
         *ie    Interlingue       
         *iu    Inuktitut	       
         *ik    Inupiaq	       
         *ga    Irish	           
         *it    Italian	       
         *ja    Japanese	       
         *jv    Javanese	       
         *kn    Kannada	       
         *kr    Kanuri	           
         *ks    Kashmiri	       
         *kk    Kazakh	           
         *km    Central Khmer	   
         *ki    Kikuyu        	   
         *rw    Kinyarwanda	   
         *ky    Kirghiz       	   
         *kv    Komi	           
         *kg    Kongo 	           
         *ko    Korean	           
         *kj    Kuanyama          
         *ku    Kurdish	       
         *lo    Lao	           
         *la    Latin	           
         *lv    Latvian	       
         *li    Limburgan         
         *ln    Lingala	       
         *lt    Lithuanian	       
         *lu    Luba-Katanga	   
         *lb    Luxembourgish     
         *mk    Macedonian	       
         *mg    Malagasy	       
         *ms    Malay	           
         *ml    Malayalam	       
         *mt    Maltese	       
         *gv    Manx	           
         *mi    Maori	           
         *mr    Marathi	       
         *mh    Marshallese	   
         *mn    Mongolian	       
         *na    Nauru	           
         *nv    Navajo        	   
         *nd    North Ndebele	   
         *nr    South Ndebele	   
         *ng    Ndonga	           
         *ne    Nepali	           
         *no    Norwegian	       
         *nb    Norwegian Bokmål	
         *nn    Norwegian Nynorsk	
         *ii    Sichuan Yi    	   
         *oc    Occitan	       
         *oj    Ojibwa	           
         *or    Oriya	           
         *om    Oromo             
         *os    Ossetian      	   
         *pi    Pali	           
         *ps    Pashto        	   
         *fa    Persian	       
         *pl    Polish	           
         *pt    Portuguese	       
         *pa    Punjabi       	   
         *qu    Quechua	       
         *ro    Romanian          
         *rm    Romansh	       
         *rn    Rundi	           
         *ru    Russian	       
         *se    Northern Sami	   
         *sm    Samoan	           
         *sg    Sango	           
         *sa    Sanskrit	       
         *sc    Sardinian	       
         *sr    Serbian	       
         *sn    Shona 	           
         *sd    Sindhi	           
         *si    Sinhala           
         *sk    Slovak	           
         *sl    Slovenian	       
         *so    Somali	           
         *st    Southern Sotho	   
         *es    Spanish           
         *su    Sundanese	       
         *sw    Swahili	       
         *ss    Swati	           
         *sv    Swedish	       
         *tl    Tagalog	       
         *ty    Tahitian	       
         *tg    Tajik	           
         *ta    Tamil	           
         *tt    Tatar	           
         *te    Telugu	           
         *th    Thai	           
         *bo    Tibetan	       
         *ti    Tigrinya	       
         *to    Tonga             
         *ts    Tsonga	           
         *tn    Tswana	           
         *tr    Turkish	       
         *tk    Turkmen	       
         *tw    Twi	           
         *ug    Uighur        	   
         *uk    Ukrainian	       
         *ur    Urdu	           
         *uz    Uzbek 	           
         *ve    Venda	           
         *vi    Vietnamese        
         *vo    Volapük	       
         *wa    Walloon	       
         *cy    Welsh	           
         *wo    Wolof	           
         *xh    Xhosa	           
         *yi    Yiddish	       
         *yo    Yoruba	           
         *za    Zhuang        	   
         *zu    Zulu  	           
         */
        #endregion
        public string SelectedLanguage
        {
            get { return PlayerPrefs.GetString("selectedLanguage", defaultLanguage); }
            set { 
                PlayerPrefs.SetString("selectedLanguage", value);
                InitTexts();
            }
        }

        public int Count
        {
            get { return codes.Length; }
        }

        [Header("ISO 639-1")]
        [SerializeField]
        private string defaultLanguage = "ru";

        [SerializeField]
        private string[] codes;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            } 
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitTexts()
        {
            Text[] texts = FindObjectsOfType<Text>();
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].SetLanguage(GetId(SelectedLanguage));
            }
        }

        public void InitText(Text text)
        {
            text.SetLanguage(GetId(SelectedLanguage));
        }
        private int GetId(string language)
        {
            if (codes != null)
                for (int i = 0; i < codes.Length; i++)
                    if (codes[i].Equals(language)) return i;
            return 0;
        }

        [ContextMenu("SwitchLanguage")]
        public void SwitchLanguage()
        {
            SelectedLanguage = codes[(GetId(SelectedLanguage) + 1) % Count];
        }

    }

}