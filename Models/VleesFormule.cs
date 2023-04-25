namespace BBQInschrivingen.API.Models
{
	public class VleesFormule
	{
		public int? Aantal { get; set; } = 0;
		public int? Hamburgers { get; set; } = null;
		public int? Kipfilets { get; set; } = null;
		public int? Worsten { get; set; } = null;
		public int? Sates { get; set; } = null;
		public int TotaalAantalSnacks
		{
			get
			{
				return (Hamburgers.HasValue ? Hamburgers.Value : 0) +
						(Kipfilets.HasValue ? Kipfilets.Value : 0) +
						(Worsten.HasValue ? Worsten.Value : 0) +
						(Sates.HasValue ? Sates.Value : 0);
			}
		}
		public int? TotaalFormule18Euro
		{
			get
			{
				return Aantal.HasValue ? Aantal!.Value * 18 : 0;
			}
		}
		public int? TotaalFormule15Euro
		{
			get
			{
				return Aantal.HasValue ? Aantal!.Value * 15 : 0;
			}
		}
	}
}
