using System.Collections.Generic;

public class ResourceConfig
{

    public static readonly Dictionary<string, List<string>> groupData = new() {
        {
            "loading", new() { "loading_fui" }
        },
        {
            "main", new() { "main_fui", "main_atlas0", "preload_fui", "common_fui", "common_atlas0" }
        },
        {
            "preload", new() { "preload_fui", "preload_atlas0" }
        },
        {
            "common", new() { "common_fui", "common_atlas0" }
        }
    };

}
