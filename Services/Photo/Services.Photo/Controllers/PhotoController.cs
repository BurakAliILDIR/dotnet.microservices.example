using Microsoft.AspNetCore.Mvc;
using Services.Photo.Dto;
using Shared.ControllerBase;

namespace Services.Photo.Controllers
{
    public class PhotoController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Save(IFormFile? photo, CancellationToken cancellationToken)
        {
            if (photo is not null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);
                }

                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };

                return ReturnActionResult(Shared.Util.Response.Return(Shared.Util.Response.ResponseStatusEnum.Success,
                    "Fotoğraf başarıyla kaydedildi.", photoDto, 201));
            }

            return ReturnActionResult(Shared.Util.Response.Return(Shared.Util.Response.ResponseStatusEnum.Warning,
                "Fotoğraf gönderilmedi.", null,
                400));
        }


        [HttpDelete]
        public IActionResult Delete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);

            if (!System.IO.File.Exists(path))
            {
                return ReturnActionResult(Shared.Util.Response.Return(Shared.Util.Response.ResponseStatusEnum.Error,
                    "Fotoğraf bulunamadı.", null, 404));
            }


            System.IO.File.Delete(path);
            return ReturnActionResult(Shared.Util.Response.Return(Shared.Util.Response.ResponseStatusEnum.Success,
                "Fotoğraf başarıyla silindi.", null, 204));
        }
    }
}