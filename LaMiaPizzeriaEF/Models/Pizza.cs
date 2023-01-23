﻿using LaMiaPizzeriaEF.ProjectClasses;

namespace LaMiaPizzeriaEF.Models {
    public class Pizza {
        #region Constructors
        public Pizza(int id, string name, string description, double price, string pictureUrl) {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PictureUrl = pictureUrl;
        }

        public Pizza(int id, string name, string description, double price, string pictureUrl, Category category) :
            this(id, name, description, price, pictureUrl) {
            Category = category;
        }

        public Pizza() { }
        #endregion

        #region Database Properties
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome della pizza non può essere più lungo di 50 caratteri.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(200, ErrorMessage = "La descrizione della pizza non può essere più lunga di 200 caratteri.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo non può essere negativo.")]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [Url(ErrorMessage = "Il link alla foto deve essere un URL valido.")]
        [EndsWith(".png", ".jpg", ".jpeg", ".webp")]
        public string PictureUrl { get; set; }
        #endregion

        #region Database Relationships
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
        #endregion

        #region Comparisons
        public bool HasTag(int tagId) {
            return Tags.Any(tag => tag.Id == tagId);
        }

        public bool HasTag(string tagId) {
            return HasTag(int.Parse(tagId));
        }

        public bool HasTag(Tag tag) {
            return Tags.Contains(tag);
        }
        #endregion
    }
}
