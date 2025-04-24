using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Profile
{
    [Key]
    public Guid id{get;set;}
    public string name{get;set;}
    public string description{get;set;}
    public double experience{get;set;}
    public string mobile{get;set;}
    public string email{get;set;}
    public string likedin{get;set;}
    public string github{get;set;}
    public string imageURL{get;set;}
    public string designation{get;set;}

}