using System;
using System.IO;
using Kembyela.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Kembyela.Services
{
    public class PdfService : IPdfService
    {
        // Coordonnées exactes pour le formulaire BCT (en points)
        // A4 paysage = 842 x 595 points
        private class Coordinates
        {
            // Titre
            public static readonly float TitreX = 421f; // Centre en largeur
            public static readonly float TitreY = 580f;

            // Timbre protestable
            public static readonly float TimbreX = 680f;
            public static readonly float TimbreY = 560f;

            // Section paiement
            public static readonly float SectionPaiementX = 50f;
            public static readonly float SectionPaiementY = 530f;
            public static readonly float SectionPaiementWidth = 742f;
            public static readonly float SectionPaiementHeight = 30f;

            // Colonne gauche
            public static readonly float DateEcheanceX = 50f;
            public static readonly float DateEcheanceY = 490f;
            public static readonly float VilleX = 50f;
            public static readonly float VilleY = 460f;
            public static readonly float DateEditionX = 50f;
            public static readonly float DateEditionY = 430f;
            public static readonly float RibX = 50f;
            public static readonly float RibY = 400f;
            public static readonly float MontantX = 50f;
            public static readonly float MontantY = 370f;

            // Colonne droite
            public static readonly float MonnaieX = 421f;
            public static readonly float MonnaieY = 490f;
            public static readonly float OrdreDeX = 421f;
            public static readonly float OrdreDeY = 460f;
            public static readonly float PayerX = 421f;
            public static readonly float PayerY = 430f;
            public static readonly float AvalX = 421f;
            public static readonly float AvalY = 400f;
            public static readonly float BanqueX = 421f;
            public static readonly float BanqueY = 370f;
            public static readonly float ProtestableX = 421f;
            public static readonly float ProtestableY = 340f;

            // Montant en lettres
            public static readonly float MontantLettresX = 50f;
            public static readonly float MontantLettresY = 280f;
            public static readonly float MontantLettresWidth = 742f;
            public static readonly float MontantLettresHeight = 40f;

            // Signatures
            public static readonly float SignatureTireX = 50f;
            public static readonly float SignatureTireY = 200f;
            public static readonly float SignatureTireWidth = 350f;
            public static readonly float SignatureTireHeight = 40f;

            public static readonly float SignatureBanqueX = 421f;
            public static readonly float SignatureBanqueY = 200f;
            public static readonly float SignatureBanqueWidth = 350f;
            public static readonly float SignatureBanqueHeight = 40f;

            public static readonly float SignatureTireurX = 50f;
            public static readonly float SignatureTireurY = 130f;
            public static readonly float SignatureTireurWidth = 742f;
            public static readonly float SignatureTireurHeight = 40f;

            // Bon pour aval
            public static readonly float BonPourAvalX = 421f;
            public static readonly float BonPourAvalY = 170f;

            // Numéro
            public static readonly float NumeroX = 680f;
            public static readonly float NumeroY = 30f;
        }

        public byte[] GenerateTraitePdf(Traite traite)
        {
            using (var memoryStream = new MemoryStream())
            {
                // 1. Créer le document en format A4 paysage
                var document = new Document(PageSize.A4.Rotate());
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // 2. Obtenir le content writer pour écrire directement
                var cb = writer.DirectContent;

                // 3. Définir les polices
                // Police de base Helvetica
                var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                // Police normale
                var normalFont = new Font(baseFont, 11);
                var boldFont = new Font(baseFont, 11, Font.BOLD);
                var titleFont = new Font(baseFont, 18, Font.BOLD);
                var amountFont = new Font(baseFont, 12, Font.BOLD);
                var smallFont = new Font(baseFont, 9);
                var verySmallFont = new Font(baseFont, 8);

                // 4. COULEURS
                var blackColor = BaseColor.Black;
                var darkGrayColor = new BaseColor(102, 102, 102); // #666
                var grayColor = new BaseColor(153, 153, 153); // #999

                // 5. DESSINER LE FORMULAIRE

                // Titre principal
                AddText(cb, "LETTRE DE CHANGE / كمبيالة", Coordinates.TitreX, Coordinates.TitreY,
                        titleFont, blackColor, Element.ALIGN_CENTER);

                // Timbre protestable
                AddText(cb, "PROTESTABLE", Coordinates.TimbreX, Coordinates.TimbreY,
                        boldFont, blackColor, Element.ALIGN_LEFT);
                AddText(cb, traite.Protestable ? "☑ OUI" : "□ NON",
                        Coordinates.TimbreX, Coordinates.TimbreY - 15, normalFont, blackColor, Element.ALIGN_LEFT);

                // Section paiement
                DrawRectangle(cb, Coordinates.SectionPaiementX, Coordinates.SectionPaiementY,
                            Coordinates.SectionPaiementWidth, Coordinates.SectionPaiementHeight, 1f);
                AddText(cb, "à laquelle le paiement doit être effectué :",
                        Coordinates.SectionPaiementX + 10, Coordinates.SectionPaiementY + 20,
                        boldFont, blackColor, Element.ALIGN_LEFT);
                AddText(cb, traite.OrdreDe,
                        Coordinates.SectionPaiementX + 10, Coordinates.SectionPaiementY + 5,
                        normalFont, blackColor, Element.ALIGN_LEFT);

                // Colonne gauche
                AddText(cb, "* Date d'échéance :", Coordinates.DateEcheanceX, Coordinates.DateEcheanceY, boldFont, blackColor);
                AddText(cb, traite.DateEcheance.ToString("dd/MM/yyyy"),
                        Coordinates.DateEcheanceX + 120, Coordinates.DateEcheanceY, normalFont, blackColor);

                AddText(cb, "* Ville :", Coordinates.VilleX, Coordinates.VilleY, boldFont, blackColor);
                AddText(cb, traite.Ville, Coordinates.VilleX + 120, Coordinates.VilleY, normalFont, blackColor);

                AddText(cb, "Date d'édition :", Coordinates.DateEditionX, Coordinates.DateEditionY, boldFont, blackColor);
                AddText(cb, traite.DateEdition.ToString("dd/MM/yyyy"),
                        Coordinates.DateEditionX + 120, Coordinates.DateEditionY, normalFont, blackColor);

                AddText(cb, "* RIB :", Coordinates.RibX, Coordinates.RibY, boldFont, blackColor);
                AddText(cb, traite.RIB, Coordinates.RibX + 120, Coordinates.RibY, normalFont, blackColor);

                AddText(cb, "* Montant (ex: 5381,800) :", Coordinates.MontantX, Coordinates.MontantY, boldFont, blackColor);
                AddText(cb, $"{traite.Montant.ToString("N3")} {traite.Monnaie}",
                        Coordinates.MontantX + 120, Coordinates.MontantY, normalFont, blackColor);

                // Colonne droite
                AddText(cb, "* Monnaie :", Coordinates.MonnaieX, Coordinates.MonnaieY, boldFont, blackColor);
                AddText(cb, traite.Monnaie, Coordinates.MonnaieX + 120, Coordinates.MonnaieY, normalFont, blackColor);

                AddText(cb, "* À l'ordre de :", Coordinates.OrdreDeX, Coordinates.OrdreDeY, boldFont, blackColor);
                AddText(cb, traite.OrdreDe, Coordinates.OrdreDeX + 120, Coordinates.OrdreDeY, normalFont, blackColor);

                AddText(cb, "* Payer :", Coordinates.PayerX, Coordinates.PayerY, boldFont, blackColor);
                AddText(cb, traite.Payer, Coordinates.PayerX + 120, Coordinates.PayerY, normalFont, blackColor);

                AddText(cb, "Aval :", Coordinates.AvalX, Coordinates.AvalY, boldFont, blackColor);
                AddText(cb, string.IsNullOrEmpty(traite.Aval) ? "________________________" : traite.Aval,
                        Coordinates.AvalX + 120, Coordinates.AvalY, normalFont, blackColor);

                AddText(cb, "* Banque :", Coordinates.BanqueX, Coordinates.BanqueY, boldFont, blackColor);
                AddText(cb, traite.Banque, Coordinates.BanqueX + 120, Coordinates.BanqueY, normalFont, blackColor);

                AddText(cb, "Protestable :", Coordinates.ProtestableX, Coordinates.ProtestableY, boldFont, blackColor);
                AddText(cb, traite.Protestable ? "☑ OUI" : "□ NON",
                        Coordinates.ProtestableX + 120, Coordinates.ProtestableY, normalFont, blackColor);

                // Montant en lettres
                DrawRectangle(cb, Coordinates.MontantLettresX, Coordinates.MontantLettresY,
                            Coordinates.MontantLettresWidth, Coordinates.MontantLettresHeight, 2f);
                AddText(cb, traite.MontantEnLettres,
                        Coordinates.MontantLettresX + Coordinates.MontantLettresWidth / 2,
                        Coordinates.MontantLettresY + 25,
                        amountFont, blackColor, Element.ALIGN_CENTER);

                // Signatures
                // Signature du tiré
                DrawRectangle(cb, Coordinates.SignatureTireX, Coordinates.SignatureTireY,
                            Coordinates.SignatureTireWidth, Coordinates.SignatureTireHeight, 1f);
                AddText(cb, "Signature (et cachet) du tiré (acheteur)",
                        Coordinates.SignatureTireX + 5, Coordinates.SignatureTireY + 25,
                        smallFont, darkGrayColor, Element.ALIGN_LEFT);

                // Signature banque
                DrawRectangle(cb, Coordinates.SignatureBanqueX, Coordinates.SignatureBanqueY,
                            Coordinates.SignatureBanqueWidth, Coordinates.SignatureBanqueHeight, 1f);
                AddText(cb, "Banque et adresse",
                        Coordinates.SignatureBanqueX + 5, Coordinates.SignatureBanqueY + 25,
                        smallFont, darkGrayColor, Element.ALIGN_LEFT);

                // Bon pour aval
                AddText(cb, "Bon pour aval",
                        Coordinates.BonPourAvalX, Coordinates.BonPourAvalY,
                        boldFont, blackColor, Element.ALIGN_LEFT);
                AddText(cb, string.IsNullOrEmpty(traite.Aval) ? "________________________" : traite.Aval,
                        Coordinates.BonPourAvalX, Coordinates.BonPourAvalY - 15,
                        normalFont, blackColor, Element.ALIGN_LEFT);

                // Signature du tireur
                DrawRectangle(cb, Coordinates.SignatureTireurX, Coordinates.SignatureTireurY,
                            Coordinates.SignatureTireurWidth, Coordinates.SignatureTireurHeight, 1f);
                AddText(cb, "Signature (et cachet) du tireur (vendeur)",
                        Coordinates.SignatureTireurX + 5, Coordinates.SignatureTireurY + 25,
                        smallFont, darkGrayColor, Element.ALIGN_LEFT);

                // Numéro de traite
                AddText(cb, $"N°: {traite.Id} | Généré le: {DateTime.Now:dd/MM/yyyy HH:mm}",
                        Coordinates.NumeroX, Coordinates.NumeroY,
                        verySmallFont, grayColor, Element.ALIGN_RIGHT);

                document.Close();

                return memoryStream.ToArray();
            }
        }

        public byte[] GenerateTraitePdfFromForm(Traite traite)
        {
            // Version alternative avec formulaire HTML intégré
            return GenerateTraitePdf(traite);
        }

        // Méthode helper pour ajouter du texte
        private void AddText(PdfContentByte cb, string text, float x, float y, Font font,
                            BaseColor color, int alignment = Element.ALIGN_LEFT)
        {
            cb.BeginText();
            cb.SetFontAndSize(font.BaseFont, font.Size);
            cb.SetColorFill(color);

            // Calculer la position selon l'alignement
            float adjustedX = x;
            if (alignment == Element.ALIGN_CENTER)
            {
                var textWidth = font.BaseFont.GetWidthPoint(text, font.Size);
                adjustedX = x - (textWidth / 2);
            }
            else if (alignment == Element.ALIGN_RIGHT)
            {
                var textWidth = font.BaseFont.GetWidthPoint(text, font.Size);
                adjustedX = x - textWidth;
            }

            cb.SetTextMatrix(adjustedX, y);
            cb.ShowText(text);
            cb.EndText();
        }

        // Méthode helper pour dessiner des rectangles
        private void DrawRectangle(PdfContentByte cb, float x, float y, float width, float height, float borderWidth)
        {
            cb.SetLineWidth(borderWidth);
            cb.Rectangle(x, y, width, height);
            cb.Stroke();
        }
    }
}