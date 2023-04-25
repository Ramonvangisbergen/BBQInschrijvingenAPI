namespace BBQInschrivingen.API.Models
{
	public class Desserten
	{
		public int? Aantal { get; set; } = 0;
		public int? Chocomousses { get; set; } = null;
		public int? Rijstpappen { get; set; } = null;
		public int? DameBlanches { get; set; } = null;
		public int TotaalDesserten
		{
			get
			{
				return (Chocomousses.HasValue ? Chocomousses.Value : 0) +
						(Rijstpappen.HasValue ? Rijstpappen.Value : 0) +
						(DameBlanches.HasValue ? DameBlanches.Value : 0);
			}
		}
	}
}
