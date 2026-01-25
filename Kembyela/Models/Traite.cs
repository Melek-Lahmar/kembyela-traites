using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kembyela.Models
{
    public class Traite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Informations de base
        [Required(ErrorMessage = "La date d'échéance est obligatoire")]
        [Display(Name = "Date d'échéance")]
        [DataType(DataType.Date)]
        //-------Date d'échéance 
        public DateTime DateEcheance { get; set; } = DateTime.Now.AddMonths(1);

        [Required(ErrorMessage = "La ville est obligatoire")]
        [Display(Name = "Ville")]
        [StringLength(100, ErrorMessage = "La ville ne doit pas dépasser 100 caractères")]
        public string Ville { get; set; }

        [Required(ErrorMessage = "La date d'édition est obligatoire")]
        [Display(Name = "Date d'édition")]
        [DataType(DataType.Date)]
        public DateTime DateEdition { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Le RIB est obligatoire")]
        [StringLength(20, MinimumLength = 20, ErrorMessage = "Le RIB doit avoir exactement 20 chiffres")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "Le RIB ne doit contenir que des chiffres")]
        [Display(Name = "RIB")]
        public string RIB { get; set; }

        // Montant
        [Required(ErrorMessage = "Le montant est obligatoire")]
        [Display(Name = "Montant (DT)")]
        public decimal Montant { get; set; }

        [Display(Name = "Monnaie")]
        [StringLength(10)]
        public string Monnaie { get; set; } = "DT";

        // Bénéficiaire et paiement
        [Required(ErrorMessage = "Le bénéficiaire est obligatoire")]
        [Display(Name = "À l'ordre de")]
        [StringLength(200, ErrorMessage = "Le nom ne doit pas dépasser 200 caractères")]
        public string OrdreDe { get; set; }

        [Required(ErrorMessage = "Le payeur est obligatoire")]
        [Display(Name = "Payer")]
        [StringLength(200, ErrorMessage = "Le nom ne doit pas dépasser 200 caractères")]
        public string Payer { get; set; }

        [Display(Name = "Aval")]
        [StringLength(200, ErrorMessage = "L'aval ne doit pas dépasser 200 caractères")]
        public string Aval { get; set; }

        [Required(ErrorMessage = "La banque est obligatoire")]
        [Display(Name = "Banque")]
        [StringLength(200, ErrorMessage = "Le nom de la banque ne doit pas dépasser 200 caractères")]
        public string Banque { get; set; }

        [Display(Name = "Protestable")]
        public bool Protestable { get; set; } = true;

        // Généré automatiquement
        [Display(Name = "Montant en lettres")]
        [StringLength(500)]
        public string MontantEnLettres { get; set; }

        // Méthode pour formater le montant
        public string GetMontantFormatted()
        {
            return $"{Montant:N3} {Monnaie}";
        }

        // Méthode pour vérifier si la date d'échéance est passée
        public bool EstEnRetard()
        {
            return DateEcheance < DateTime.Today;
        }
    }
}