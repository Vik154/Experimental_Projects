﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Notes.Identity.Models;

namespace Notes.Identity.Data;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser> {
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder.HasKey(x => x.Id);
    }
}
