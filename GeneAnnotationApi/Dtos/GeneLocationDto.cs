namespace GeneAnnotationApi.Dtos
{
    public class GeneLocationDto: BaseDto
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Locus { get; set; }
        public int HgVersion { get; set; }
        public GeneDto Gene { get; set; }
        public ChromosomeDto Chromosome { get; set; }
    }
}