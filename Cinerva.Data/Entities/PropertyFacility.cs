namespace Cinerva.Data.Entities
{
    public class PropertyFacility
    {
        public int PropertyId { get; set; }
        public int FacilityId { get; set; }
        public Property Property { get; set; }
        public GeneralFeature GeneralFeature { get; set; }
    }
}
