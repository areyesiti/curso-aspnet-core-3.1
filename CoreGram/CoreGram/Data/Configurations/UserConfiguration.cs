using CoreGram.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Data.Configurations
{
    public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.HasKey(x => new { x.UserId, x.FollowerId });

            builder.HasOne<User>(x => x.UserFollower)
                .WithMany(x => x.UsersFollowers)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(x => x.UserFollowing)
                .WithMany(x => x.UsersFollowings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
