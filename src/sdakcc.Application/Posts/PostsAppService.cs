
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sdakcc.PostsDto;
using sdakcc.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;


namespace sdakcc.Application.Posts
{
    public class PostsAppService: sdakccAppService
    {
        private readonly IRepository<Entities.Posts, Guid> _postsRepos;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IPostsRepository _postsReposCustom;

        public PostsAppService(IRepository<Entities.Posts, Guid> postsRepos, IWebHostEnvironment webHostEnvironment,
                                IPostsRepository postsRepository)
        {
            _postsRepos = postsRepos;
            this.webHostEnvironment = webHostEnvironment;
            _postsReposCustom = postsRepository;
        }

        

        //Create Post

        public async Task<CreatedPostOutDto> AddPostAsync(CreatePostDto createPostDto)
        {
            try
            {
                var outPut = ObjectMapper.Map<CreatePostDto, Entities.Posts>(createPostDto);

                //save image if exists

                if (createPostDto.ImageFile != null)
                {
                    var saveResult = await SaveFile(createPostDto.ImageFile);
                    if (saveResult.ReturnCode == "200")
                    {
                        outPut.ImageUrl = saveResult.Link;
                    }
                    else
                    {
                        throw new ApplicationException(saveResult.Message);
                    }

                }

                var createPosts = await _postsRepos.InsertAsync(outPut);

                return ObjectMapper.Map<Entities.Posts, CreatedPostOutDto>(createPosts);
            }
            catch (Exception)
            {
                //Todo; Log exception
                return null;
            }
        }

        //PUT Post
        public async Task<ActionResult<CreatedPostOutDto>> ChangePostAsync(UpdatePostsDto updatePostsDto)
        {
            
            var objFromDb = await _postsRepos.FirstOrDefaultAsync(x => x.Id == updatePostsDto.Id);
            if (objFromDb == null) return null;

            try
            {
                objFromDb.Description = updatePostsDto.Description;

                //save image if exists

                if (updatePostsDto.ImageName != null)
                {
                    var saveResult = await SaveFile(updatePostsDto.ImageName);
                    if (saveResult.ReturnCode == "200")
                    {
                        objFromDb.ImageUrl = saveResult.Link;
                    }
                    else
                    {
                        throw new ApplicationException(saveResult.Message);
                    }

                }

                var updatePost = await _postsRepos.UpdateAsync(objFromDb);

                return ObjectMapper.Map<Entities.Posts, CreatedPostOutDto>(objFromDb);
            }
            catch (Exception )
            {
                //log exception
                return null;
            }
        
        }

        //Get by Id
        public async Task<ActionResult<PostsListDto>> FindById(Guid Id)
        {

            var objFromDb = await _postsRepos.GetAsync(x => x.Id == Id);
            if (objFromDb == null) return null;
            return ObjectMapper.Map<Entities.Posts, PostsListDto>(objFromDb);
           

        }
        

        //Get All
        public async Task<IQueryable<Entities.Posts>> ListAll(int page=1)
        {

            //  int numberPerPage = 10;
            //  int skip = numberPerPage * (page - 1);
            //  //var postsList = await _postsRepos.GetPagedListAsync(skip,numberPerPage,"CreationTime",includeDetails:true);

            //  //var outPut = new GetAllPostsDto()
            //  //{
            //  //    Page = page,
            //  //    Posts = ObjectMapper.Map<List<Entities.Posts>, IEnumerable<PostsListDto>>(postsList)
            //  //};
            //  //return outPut;

            //  //Get a IQueryable<T> by including sub collections
            // var queryable =  await _postsRepos.WithDetailsAsync(x=>x.Likes);

            //  queryable = queryable.Include(x => x.Likes).ThenInclude(y => y.Users)
            //               .OrderByDescending(x => x.CreationTime).Skip(skip).Take(numberPerPage);

            //  var posts = await queryable.ToListAsync();
            //// queryable = queryable.OrderBy(x=>x.CreationTime).Skip(skip).Take(numberPerPage);
            ////Execute the query and get the result
            //// var posts = await AsyncExecuter.ToListAsync(queryable);

            //  // var posts = await queryable.Include(x=>x.Likes).ThenInclude(y=>y.Users).ToListAsync();

            //  //var likes = ObjectMapper.Map<Entities.Posts>


            var posts = await _postsReposCustom.GetAllFullyLoadedPostsAsync(page);

            List<PostsListDto> outPutList = ObjectMapper.Map<List<Entities.Posts>, List<PostsListDto>>(posts.ToList());

            return posts;

           
            //var outPut = new GetAllPostsDto()
            //{
            //    Page = page,
            //    Posts = ObjectMapper.Map<List<Entities.Posts>, IEnumerable<PostsListDto>>(posts)
            //};
            //return outPut;


        }

        //Delete by Id
        public async Task<ActionResult> Remove(Guid Id)
        {
            try
            {
                var checkExist = await _postsRepos.GetAsync(x => x.Id == Id);
            }
            catch (Exception)
            {
                 return new NotFoundResult();
            }
            
            await _postsRepos.DeleteAsync(Id);
            return new OkResult();
        }



        //Save imageupload file
        public async Task<ResultObject> SaveFile(IFormFile dto)
        {
            //handle image upload
            string webRootPath = webHostEnvironment.WebRootPath;
            string link = null;
            var obj = new ResultObject();

            var files = dto;
            try
            {
                if (files.Length > 0)
                {
                    string uploadPath = Path.Combine(webRootPath, @"\images".TrimStart('\\')); // doesnt work if second path has a trailling slash
                    string extension = Path.GetExtension(files.FileName);
                    if (!(extension == ".jpg" || extension == ".png"))
                    {
                        throw new ApplicationException("The image file type must be jpg or png");

                    }

                    string fileNewName = Guid.NewGuid().ToString() + extension;

                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileNewName), FileMode.Create))
                    {
                        await files.CopyToAsync(fileStream);
                    }
                    link = @"\images\" + fileNewName;
                }

                string msg = "Upload of Image Successful";
                obj.ReturnCode = "200";
                obj.ReturnDescription = msg;
                obj.Response = "Success";
                obj.Message = msg;
                
            }
            catch (Exception ex)
            {
                string msg = "An Error has occurred while attempting to Upload Image: Inner Exception: " + ex.Message;
                obj.ReturnCode = "501";
                obj.ReturnDescription = msg;
                obj.Response = "Failed";
                obj.Message = msg;
            }
            return obj;
        }
    }
}
