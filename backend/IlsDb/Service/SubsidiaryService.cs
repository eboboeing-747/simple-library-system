using IlsDb.Repository;
using IlsDb.Object;
using IlsDb.Entity.BaseEntity;

namespace IlsDb.Service
{
    public class SubsidiaryService
    {
        private readonly SubsidiaryRepository _subsidiaryRepository;

        public SubsidiaryService(SubsidiaryRepository subsidiaryRepository)
        {
            this._subsidiaryRepository = subsidiaryRepository;
        }

        public async Task Create(SubsidiaryRecieve subsidiary)
        {
            SubsidiaryEntity subsidiaryToCreate = new SubsidiaryEntity
            {
                Name = subsidiary.Name,
                Address = subsidiary.Address,
                Description = subsidiary.Description
            };

            await this._subsidiaryRepository.Create(subsidiaryToCreate);
        }
    }
}
