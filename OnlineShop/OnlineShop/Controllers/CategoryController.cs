﻿using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.DTOs;
using OnlineShop.Repositories.CategoryRepositories;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categRepo; //tabela pe care o folosesc

        public CategoryController(ICategoryRepository repo) //constructor
        {
            _categRepo = repo;
        }
        [HttpGet]
        public Task<List<Category>> Index() //get all, returneaza lista de categorii
        {
            return _categRepo.GetAllAsync();
        }

        [HttpPut]
        public async Task<IActionResult> New( CategoryDTO categ) //adauga o categorie noua
        {   
            _categRepo.CreateAsync(new Category(categ)); //adauga o categorie noua, in clasa Category am un constructor care primeste un CategoryDTO
            bool saved = await _categRepo.SaveAsync();  //salvez modificarile si verific daca e ok
            return  saved ? Ok() : BadRequest("A aparut o eroare") ;
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id) //sterg o categorie
        {   
            Category? categ= await _categRepo.FindByIdAsync(id); //obtin categoria pe care vreau sa o sterg
            
            if (categ == null) //daca nu exista
            {
                return NotFound("Nu exista categoria");
            }

            _categRepo.Delete(categ); //sterg categoria
            bool saved = await _categRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDTO categ, Guid id) //modific o categorie
        {
            Category? category = await _categRepo.FindByIdAsync(id); //obtin categoria pe care vreau sa o modific
            if (category == null) //daca nu exista
            {
                return NotFound("Nu exista categoria");
            }
            category.CategoryName = categ.CategoryName; //modific numele
            category.DateModified = DateTime.UtcNow;

            _categRepo.Update(category); //modific categoria
            bool saved = await _categRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }

    }
}
