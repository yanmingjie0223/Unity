using System.Collections.Generic;

public class ResourceConfig
{

    public static readonly Dictionary<string, List<string>> groupData = new() {
        {
            "relyon", new() { "common_fui", "common_atlas0", "preload_fui", "preload_atlas0" }
        },
        {
            "common", new() { "common_fui", "common_atlas0" }
        },
        {
            "preload", new() { "preload_fui", "preload_atlas0" }
        },
        {
            "loading", new() { "loading_fui" }
        },
        {
            "main", new() { "main_fui", "main_atlas0" }
        },
        {
            "bag", new() { "bag_fui", "bag_atlas0" }
        },
    };

}
