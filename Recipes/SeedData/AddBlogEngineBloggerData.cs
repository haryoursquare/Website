﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Recipes.Models;

namespace Recipes.SeedData
{
    public class AddBlogEngineBloggerData
    {
        public static void PopulateBloggers(RecipesEntities context)
        {
            var bloggers = new List<Blogger> {
                new Blogger { BloggerID = 1, BloggerName = "Evgeny" },
                new Blogger { BloggerID = 2, BloggerName = "Someone Else" },
            };

            bloggers.ForEach(b => context.Bloggers.Add(b));
            context.SaveChanges();
        }
    }
}