using WebApplication2.Dto;
using WebApplication2.Exceptions;
using WebApplication2.Repositories;

namespace WebApplication2.Services;

public interface IWarehouseService
{
    public Task<int> RegisterProductInWarehouseAsync(RegisterProductInWarehouseRequestDTO dto);
}

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IOrderRepository _orderRepository;
    public WarehouseService(IWarehouseRepository warehouseRepository, IOrderRepository orderRepository)
    {
        _warehouseRepository = warehouseRepository;
        _orderRepository = orderRepository;
    }
    
    public async Task<int> RegisterProductInWarehouseAsync(RegisterProductInWarehouseRequestDTO dto)
    {
        // Example Flow:
        // check if product exists else throw NotFoundException
            var productExists = await _warehouseRepository.ProductExistsAsync(dto.IdProduct);
            if (!productExists)
            {
                throw new NotFoundException("Nie znaleziono produktu.");
            }

        // check if warehouse exists else throw NotFoundException
            var warehouseExists = await _warehouseRepository.WarehouseExistsAsync(dto.IdWarehouse);
            if (!warehouseExists)
            {
                throw new NotFoundException("Nie znaleziono magazynu.");
            }

        //wartość ilości przekazana w żądaniu powinna być większa niż 0

            if (dto.Amount <= 0)
            {
                throw new NotFoundException("Ilość musi być większa.");
            }

        // get order if exists else throw NotFoundException
        const int idOrder = 1;
        // check if product is already in warehouse else throw ConflictException

        var idProductWarehouse = await _warehouseRepository.RegisterProductInWarehouseAsync(
            idWarehouse: dto.IdWarehouse!.Value,
            idProduct: dto.IdProduct!.Value,
            idOrder: idOrder,
            createdAt: DateTime.UtcNow);

        if (!idProductWarehouse.HasValue)
            throw new Exception("Failed to register product in warehouse");

        return idProductWarehouse.Value;
    }
}