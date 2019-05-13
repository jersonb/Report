namespace ClassLibrary.util
{
    public static class Constantes
    {
        public static string PathData(Tipo t) { return @"\\Mac\Home\Documents\Report\data\" + Reads.SelectTipo(t); }
        public const string PATH_FILES = @"\\Mac\Home\Documents\Report\files\";
    }
}
