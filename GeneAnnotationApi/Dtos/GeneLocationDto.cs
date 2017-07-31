namespace GeneAnnotationApi.Dtos
{
    public class GeneLocationDto
    {
        public int Id { get; set; }
        public GeneDto Gene { get; set; }
        public string Chr { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Locus { get; set; }
        public int HgVersion { get; set; }
    }
}