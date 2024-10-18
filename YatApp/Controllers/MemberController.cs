using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using YatApp.Api;
using Library.Data;
using Library.Models;
using Interface;
using Dto;

namespace Library.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberController : BaseApiController
{
    public MemberController(IUnitofWork unitofWork) : base(unitofWork)
    {
    }

    #region Get
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var res = _unitofWork.Members.GetAll();
        return Ok(res);
    }
    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var res = await _unitofWork.Members.GetAllAsync();
        return Ok(res);
    }
    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        var res = _unitofWork.Members.GetById(id);
        return Ok(res);
    }
    [HttpGet("GetByIdAsync")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var res = await _unitofWork.Members.GetByIdAsync(id);
        return Ok(res);
    }
    #endregion

    #region Add
    [HttpPost("Add")]
    public IActionResult Add(MemberDto dto)
    {
        Member member = new Member()
        {
            MemberName = dto.MemberName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Password = dto.Password,
            MembershipDate = dto.MembershipDate,
            RoleId = dto.RoleId,
            Username = dto.Username
        };
        var res = _unitofWork.Members.Add(member);
        _unitofWork.Save();
        return Ok(member);
    }

    [HttpPost("AddAsync")]
    public async Task<IActionResult> AddAsync(MemberDto dto)
    {
        //AutoMapper package
        Member member = new Member()
        {
            MemberName = dto.MemberName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Password = dto.Password,
            MembershipDate = dto.MembershipDate,
            RoleId = dto.RoleId,
            Username = dto.Username
        };
        var res = await _unitofWork.Members.AddAsync(member);
        _unitofWork.Save();
        return Ok(member);
    }
    #endregion

    #region Update
    [HttpPut("Update")]
    public IActionResult Update(Member model)
    {
        var res = _unitofWork.Members.Update(model);
        _unitofWork.Save();
        return Ok(model);
    }

    [HttpPut("UpdateAsync")]
    public async Task<IActionResult> UpdateAsync(Member model)
    {
        var res = _unitofWork.Members.Update(model);
        _unitofWork.Save();
        return Ok(model);
    }
    #endregion

    #region Delete
    [HttpDelete("Delete")]
    public IActionResult Delete(Member model)
    {
        _unitofWork.Members.Delete(model);
        _unitofWork.Save();
        return Ok("Deleted");
    }


    #endregion


}
