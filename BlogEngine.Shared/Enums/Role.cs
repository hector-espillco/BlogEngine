using System.ComponentModel;

namespace BlogEngine.Shared.Enums
{
    public enum  Role
    {
        [Description("Public")]
        Public = 1,

        [Description("Writer")]
        Writer = 2,

        [Description("Editor")]
        Editor = 3
    }
}
