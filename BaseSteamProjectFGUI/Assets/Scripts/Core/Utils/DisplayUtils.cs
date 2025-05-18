using FairyGUI;
using UnityEngine;

public class DisplayUtils
{
    public static void SetLableI18nKey(GTextField tf, string key)
    {
        tf.text = GetI18nByKey(key);
    }

    public static void SetButtonI18nKey(GButton btn, string key)
    {
        btn.title = GetI18nByKey(key);
    }

    public static string GetI18nByKey(string key)
    {
        var tables = ConfigManager.GetInstance().tables;
        tables.TbLanguage.DataMap.TryGetValue(key, out cfg.ncb.Language value);
        if (value == null)
        {
            Debug.LogWarning("language not found key : " + key);
            return "";
        }

        var model = ModelManager.GetInstance().GetModel<SettingOptionModel>();
        var lang = model.language.lang;
        switch (lang)
        {
            case Setting.LanguageType.zh:
                return value.Zh;
            case Setting.LanguageType.en:
                return value.En;
            case Setting.LanguageType.fr:
                return value.Fr;
            case Setting.LanguageType.ja:
                return value.Ja;
            case Setting.LanguageType.ko:
                return value.Ko;
            case Setting.LanguageType.tc:
                return value.Tc;
            case Setting.LanguageType.de:
                return value.De;
            case Setting.LanguageType.vn:
                return value.Vn;
            case Setting.LanguageType.es:
                return value.Es;
            case Setting.LanguageType.ru:
                return value.Ru;
            case Setting.LanguageType.pt:
                return value.Pt;
            case Setting.LanguageType.it:
                return value.It;
            default:
                Debug.LogError("not deal LanguageType: " + lang);
                break;
        }
        return "";
    }
}
