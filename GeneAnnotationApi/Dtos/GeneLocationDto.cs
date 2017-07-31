namespace GeneAnnotationApi.Dtos
{
    public class GeneLocationDto: BaseDto
    {
        public string Chr { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Locus { get; set; }
        public int HgVersion { get; set; }
    }
}