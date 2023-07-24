namespace TpaoProject1.Model
{
    public class WellAndFormation
    {
        public WellTop? well { get; set; }

        public List<Formation>? formation { get; set; }

        public Dictionary<string, string> color { get; set; }
    }
}
