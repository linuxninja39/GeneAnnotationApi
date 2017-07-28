namespace GeneAnnotationApi.JsonModels
{
    public class GeneLocationJsonModel
    {
        public int id { get; set; }
        public GeneJsonModel Gene { get; set; }
        public string Chr { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Locus { get; set; }
        public int HgVersion { get; set; }
    }
}