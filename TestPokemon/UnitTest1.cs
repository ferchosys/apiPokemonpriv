using AutoMapper;
using Servicios.Core.Interfaces;
using Servicios.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Servicios.Core.DTOs;
using Servicios.Core.Entities;
using Org.BouncyCastle.Asn1.Ocsp;

namespace TestPokemon
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public async Task ConsultarPokemon_ShouldReturnPokemonDto_WhenResponseIsOK()
        {
            // Arrange
            var mockRepository = new Mock<IConsultarPokemonRepository>();
            var mockMapper = new Mock<IMapper>();

            var response = new PokemonResponse
            {
                response = "OK"
            };
            
            var service = new ConsultarPokemonService(mockMapper.Object, mockRepository.Object);

            var expectedDto = new PokemonDto
            {
                nombre_pokemon = "Pikachu",
                habilidades = new Habilidades
                {
                    ocultas = new List<Ocultas>
                {
                    new Ocultas { nombre_habilidades = "Invisibility" }
                }
                },
                response = "OK"
            };

            mockMapper.Setup(mapper => mapper.Map<PokemonDto>(It.IsAny<PokemonDto>())).Returns(expectedDto);

            // Act
            var result = await service.ConsultarPokemon("Pikachu");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Pikachu", result.nombre_pokemon);
            Assert.AreEqual("Invisibility", result.habilidades.ocultas[0].nombre_habilidades);
            Assert.AreEqual("OK", result.response);
        }

        [TestMethod]
        public async Task ConsultarPokemon_ShouldReturnError_WhenResponseIsNOK()
        {
            // Arrange
            var mockRepository = new Mock<IConsultarPokemonRepository>();
            var mockMapper = new Mock<IMapper>();

            var response = new PokemonResponse
            {
                response = "NOK",
                error = "Pokemon not found"
            };

            var service = new ConsultarPokemonService(mockMapper.Object, mockRepository.Object);

            var expectedDto = new PokemonDto
            {
                error = "Pokemon not found",
                response = "NOK"
            };

            mockMapper.Setup(mapper => mapper.Map<PokemonDto>(It.IsAny<PokemonDto>())).Returns(expectedDto);

            // Act
            var result = await service.ConsultarPokemon("Pikachu");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Pokemon not found", result.error);
            Assert.AreEqual("NOK", result.response);
        }

        [TestMethod]
        public void ConsultarPokemn2()
        {
            
        }
    }
}