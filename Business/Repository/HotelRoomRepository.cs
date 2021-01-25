using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper; 
        

        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }
        public async Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto hotelRoomDto)
        {
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDto,HotelRoom>(hotelRoomDto);
            hotelRoom.CreatedBy = "";
            var addedHotel = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom,HotelRoomDto>(addedHotel.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {      
                var roomToDelete = await _db.HotelRooms.FindAsync(roomId);
                if(roomToDelete != null)
                {
                    _db.HotelRooms.Remove(roomToDelete);
                    return await _db.SaveChangesAsync();
                }
            return 0;
        }

        public async Task<IEnumerable<HotelRoomDto>> GetHotelRooms()
        {
            try
            {
                IEnumerable<HotelRoomDto> hotelRoomsDto = _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDto>>(await _db.HotelRooms.Include(x => x.HotelRoomImages).ToListAsync());
                return hotelRoomsDto;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<HotelRoomDto> GetHotelRoomById(int roomId)
        {
            try
            {
                var room = await _db.HotelRooms.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(p => p.Id == roomId);                
                HotelRoomDto roomDto = _mapper.Map<HotelRoom, HotelRoomDto>(room);
                return roomDto;    
            }
            catch (Exception ex)
            {

                return null;
            }
     
        }

        public async Task<HotelRoomDto> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if(roomId == 0)
                {
                    var room = await _db.HotelRooms.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
                    HotelRoomDto roomDto = _mapper.Map<HotelRoom, HotelRoomDto>(room);
                    return roomDto;
                }
                else
                {
                    var room = await _db.HotelRooms.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower() && p.Id != roomId);
                    HotelRoomDto roomDto = _mapper.Map<HotelRoom, HotelRoomDto>(room);
                    return roomDto;
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDto)
        {
            try
            {
                if (roomId == hotelRoomDto.Id)
                {
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map(hotelRoomDto, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom,HotelRoomDto>(updatedRoom.Entity);                
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }  
        }
    }
}
