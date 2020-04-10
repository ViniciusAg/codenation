using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Source.Map
{
    public class CandidateMap : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(x => new { x.UserId, x.AccelerationId, x.CompanyId });
        }
    }
}