namespace SV.Common.Options
{
    public class CodeGenerateOption
    {
        public string ModelsNamespace { get; set; }
	    // ReSharper disable once InconsistentNaming
        public string IRepositoriesNamespace { get; set; }
        public string RepositoriesNamespace { get; set; }
	    // ReSharper disable once InconsistentNaming
        public string IServicsNamespace { get; set; }
        public string ServicesNamespace { get; set; }
    }
}
