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
    public class HotelImagesRepository : IHotelImagesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelImagesRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<int> CreateHotelRoomImage(HotelRoomImageDto imageDto)
        {
            try
            {
                var image = _mapper.Map<HotelRoomImageDto, HotelRoomImage>(imageDto);
                await _db.HotelRoomImages.AddAsync(image);
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return 0;
            }           

        }

        public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
        {
            var image = await _db.HotelRoomImages.FindAsync(imageId);
            _db.HotelRoomImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl)
        {
            var imageToBeDeleted = await _db.HotelRoomImages.FirstOrDefaultAsync(x => x.RoomImageUrl.ToLower() == imageUrl.ToLower());
            _db.HotelRoomImages.Remove(imageToBeDeleted);
            return await _db.SaveChangesAsync();             
        }

        public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
        {
            var resultList = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
            _db.HotelRoomImages.RemoveRange(resultList);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelRoomImageDto>> GetHotelRoomImages(int roomId)
        {
            try
            {
                var result = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
                var resultDto = _mapper.Map<IEnumerable<HotelRoomImage>, IEnumerable<HotelRoomImageDto>>(result);
                return resultDto;
            }
            catch (Exception ex)
            {

                throw;
            }
     
        }
    }
}
