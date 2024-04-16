﻿using App_Data.DbContext;
using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_Data.ViewModels.ChipDTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChipController : ControllerBase
    {
        private readonly IAllRepo<Chip> _chiprepos;
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public ChipController(IAllRepo<Chip> chiprepos, IMapper mapper)
        {
            _chiprepos = chiprepos;
            _mapper = mapper;
            _dbContext = new AppDbContext();
        }

        // GET: api/<ChipController>
        [HttpGet]
        public List<Chip> GetAll()
        {
            return _chiprepos.GetAll().ToList();
        }

        // GET api/<ChipController>/5
        [HttpGet("{id}")]
        public Chip? GetChip(string id)
        {
            return _chiprepos.GetAll().FirstOrDefault(x => x.IdChip == id);
        }

        // POST api/<ChipController>
        [HttpPost]
        public bool Post(ChipDTO chipDTO)
        {
            chipDTO.IdChip = Guid.NewGuid().ToString();
            var chip = _mapper.Map<Chip>(chipDTO);
            chip.TrangThai = 0;
            chip.MaChip = !_chiprepos.GetAll().Any() ? "C1" : "C" + (_chiprepos.GetAll().Count() + 1);
            return _chiprepos.AddItem(chip);
        }

        // PUT api/<ChipController>/5
        [HttpPut("{id}")]
        public bool Put(ChipDTO chipDTO)
        {
            try
            {
                var nameChip = chipDTO.TenChip!.Trim().ToLower();
                if (!_dbContext.Chips.Where(x => x.TenChip!.Trim().ToLower() == nameChip).Any())
                {
                    var chip = _mapper.Map<Chip>(chipDTO);
                    _dbContext.Attach(chip);
                    _dbContext.Entry(chip).Property(sp => sp.TenChip).IsModified = true;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // DELETE api/<ChipController>/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            var chip = GetChip(id);
            if (chip != null)
            {
                chip!.TrangThai = 1;
                return _chiprepos.RemoveItem(chip);
            }
            return false;
        }
    }
}