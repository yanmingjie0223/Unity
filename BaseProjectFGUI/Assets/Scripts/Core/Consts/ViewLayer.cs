public class ViewLayer
{
    public static FairyGUI.GComponent BACKSTAGE_COMPONENT;
    public static FairyGUI.GComponent BOTTOM_COMPONENT;
    public static FairyGUI.GComponent MIDDLE_COMPONENT;
    public static FairyGUI.GComponent TOP_COMPONENT;
    public static FairyGUI.GComponent WINDOW_COMPONENT;
    public static FairyGUI.GComponent GUIDE_COMPONENT;
    public static FairyGUI.GComponent MAX_COMPONENT;

    public static void Initialize()
    {
        BACKSTAGE_COMPONENT = new FairyGUI.GComponent();
        BOTTOM_COMPONENT = new FairyGUI.GComponent();
        MIDDLE_COMPONENT = new FairyGUI.GComponent();
        TOP_COMPONENT = new FairyGUI.GComponent();
        WINDOW_COMPONENT = new FairyGUI.GComponent();
        GUIDE_COMPONENT = new FairyGUI.GComponent();
        MAX_COMPONENT = new FairyGUI.GComponent();
    }
}
