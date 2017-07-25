﻿namespace GeneAnnotationApi.JsonModels
{
    public enum GeneVariantJsonType
    {
        DELETION_WHOLE_GENE,
        PARTIAL_DELETION_INTRAGENIC,
        PARTIAL_DELETION_DELETED_5,
        PARTIAL_DELETION_DELETED_3,
        DUPLICATION_WHOLE_GENE,
        PARTIAL_DUPLICATION_INTRAGENIC,
        PARTIAL_DUPLICATION_DUPLICATED_5,
        PARTIAL_DUPLICATION_DUPLICATED_3,
        SNV_PREDICTED_LOF,
        SNV_PREDICTED_GOF,
        SPLICE_SITE,
        GWAS
    }
}