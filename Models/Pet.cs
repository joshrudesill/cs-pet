using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Diagnostics.CodeAnalysis;

namespace pet_hotel
{
    public enum PetBreedType {Shepherd, Poodle, Beagle, Bulldog, Terrier, Boxer, Labrador, Retriever}
    public enum PetColorType {White, Black, Golden, Tricolor, Spotted}
    public class Pet {
        public int id {get;set;}

        [Required]
        public string name {get;set;}
         [Required, JsonConverter(typeof(JsonStringEnumConverter))]
        public PetBreedType breed {get;set;}
        [Required, JsonConverter(typeof(JsonStringEnumConverter))]
        public PetColorType color {get;set;}
        [AllowNull]
        public DateTime? CheckedInAt {get;set;}
        
        [Required, ForeignKey("PetOwner")]
        public int PetOwnerId {get;set;}
        public PetOwner PetOwner {get;set;}

    }
}
