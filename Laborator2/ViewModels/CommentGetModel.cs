using Laborator2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.ViewModels
{
    public class CommentGetModel
    {
      public string Text { get; set; }
      public string Author { get; set; }


       public static CommentGetModel GetCommentModel(Comment comment)
       {
         return new CommentGetModel      
         {
             Author = comment.Author,
             Text = comment.Text
                 
         };
    }
       }
}
