using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IProfileServicer
{
    public Task<Profile> addProfile(string name,string description,double experience,string mobile,string email,string likedin,string github,string imageURL,string designation,string pic1Url,string pic2Url,string resumeUrl);
    public Task<Profile> updateProfile(Guid id,string name,string description,double experience,string mobile,string email,string likedin,string github,string imageURL,string deignation);
    public Task<List<Profile>> getProfile();
}