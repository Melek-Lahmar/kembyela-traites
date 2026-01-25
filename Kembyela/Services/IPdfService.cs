using Kembyela.Models;

namespace Kembyela.Services
{
    public interface IPdfService
    {
        byte[] GenerateTraitePdf(Traite traite);
        byte[] GenerateTraitePdfFromForm(Traite traite);
    }
}