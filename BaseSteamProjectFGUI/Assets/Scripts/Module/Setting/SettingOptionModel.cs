public class SettingOptionModel : BaseModel
{
    public Setting.Language language = new();

    public override void Initialize()
    {
        language.lang = Setting.LanguageType.zh;
    }
}
