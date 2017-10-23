using GigHubOk.Models;

namespace GigHubOk.ViewModels
{
    public class GigsDetailsViewModel
    {
        public Gig Gig { get; set; }
        public bool Attending { get; set; }
        public bool Following { get; set; }
    }
}