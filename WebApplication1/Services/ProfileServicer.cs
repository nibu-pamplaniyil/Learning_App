using System.Resources;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

public class ProfileServicer:IProfileServicer
{
    private readonly ApplicationDBContext _context;
    public ProfileServicer(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<Profile> addProfile(string name,string description,double experience,string mobile,string email,string likedin,string github,string imageURL,string designation,string pic1,string pic2,string resume)
    {   
        try{
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description)
            || string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(email)
            || string.IsNullOrWhiteSpace(likedin) || string.IsNullOrWhiteSpace(github)
            || string.IsNullOrWhiteSpace(imageURL)||string.IsNullOrWhiteSpace(designation))
            {
                throw new ArgumentException("Please fill all fields");
            }
        
            var profile = new Profile
            {
                name = name,
                description = description,
                experience = experience,
                mobile = mobile,
                email = email,
                likedin = likedin,
                github=github,
                imageURL = imageURL,
                designation = designation,
                pic1 = pic1,
                pic2 = pic2,
                resume = resume
                
            };
            _context.Profile.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }
        catch(Exception e)
        {
            throw;
        }
    }

    public async Task<Profile> updateProfile(Guid id,string name,string description,double experience,string mobile,string email,string likedin,string github,string imageURL,string designation)
    {
        try{
            var profile = await _context.Profile.FindAsync(id);
            if(profile==null)
            {
                throw new ArgumentException("No profile found");
            }
            profile.name = name;
            profile.description = description;
            profile.experience = experience;
            profile.mobile = mobile;
            profile.email = email;
            profile.likedin = likedin;
            profile.github = github;
            profile.imageURL = imageURL;
            profile.designation = designation;

            await _context.SaveChangesAsync();
            return profile;
        }
        catch(Exception e)
        {
            throw;
        }
    }

    public async Task<List<Profile>> getProfile()
    {
        try{
            var result = await _context.Profile.ToListAsync();
            if(result==null)
            {
                throw new ArgumentException("No profile found");
            }
            return result;
        }
        catch(Exception e)
        {
            throw;
        }
    }
}