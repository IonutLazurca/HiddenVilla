using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto hotelRoomDto);
        public Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDto);
        public Task<HotelRoomDto> GetHotelRoomById(int roomId);
        public Task<IEnumerable<HotelRoomDto>> GetHotelRooms();
        public Task<HotelRoomDto> IsRoomUnique(string name, int roomId=0);
        public Task<int> DeleteHotelRoom(int roomId);

    }
}
