using Npv_Exercise.Core.Constants;
using Npv_Exercise.Service.Services.NPV;
using System.Collections.Generic;
using Xunit;

namespace Npv_Exercise.Service.Tests.Services.NPV
{
    public class NpvServiceTests
    {
        [Fact]
        public void ComputeNPVTest()
        {
            // Arrange
            var npvService = new NpvService();

            // Act
            var sut = npvService.ComputeNPV(-1000, 2, new List<decimal>
            {
                100,
                200,
                300
            });

            // Assert
            Assert.Empty(sut.Errors); 
            Assert.Equal(572.96967229798493791980460004m, sut.PresentValueOfExpectedCashflows); //expected values should be computed manually
            Assert.Equal(-427.03032770201506208019539996m, sut.NetPresentValue); //expected values should be computed manually
        }

        [Fact]
        public void ComputeNPV_ValidationCheck_Test()
        {
            // Arrange
            var npvService = new NpvService();

            // Act
            var sut = npvService.ComputeNPV(1000, -1, new List<decimal>());

            // Assert
            Assert.NotEmpty(sut.Errors);
            Assert.Equal(sut.Errors[0].Code, ErrorCodes.NPVServiceError001);
            Assert.Equal(sut.Errors[1].Code, ErrorCodes.NPVServiceError002);
            Assert.Equal(sut.Errors[2].Code, ErrorCodes.NPVServiceError004);
        }

        [Fact]
        public void ValidateNPVInputs_Test()
        {
            // Arrange
            var npvService = new NpvService();

            // Act
            var sut = npvService.ValidateNPVInputs(1000, -1, new List<decimal>(), -0.25m);

            // Assert
            Assert.NotEmpty(sut);
            Assert.Equal(sut[0].Code, ErrorCodes.NPVServiceError001);
            Assert.Equal(sut[1].Code, ErrorCodes.NPVServiceError002);
            Assert.Equal(sut[2].Code, ErrorCodes.NPVServiceError003);
            Assert.Equal(sut[3].Code, ErrorCodes.NPVServiceError004);
        }

        [Fact]
        public void ComputeNPVsTest()
        {
            // Arrange
            var npvService = new NpvService();

            // Act
            var sut = npvService.ComputeNPVs(-1000, 2, 3, 0.25m, new List<decimal>
            {
                100,
                200,
                300
            });

            // Assert
            Assert.Empty(sut.Errors);
            Assert.Equal(572.96967229798493791980460004m, sut.Data[0].PresentValueOfExpectedCashflows); //expected values should be computed manually
            Assert.Equal(-427.03032770201506208019539996m, sut.Data[0].NetPresentValue); 

            Assert.Equal(569.72259420480266217938283399m, sut.Data[1].PresentValueOfExpectedCashflows); 
            Assert.Equal(-430.27740579519733782061716601m, sut.Data[1].NetPresentValue);

            Assert.Equal(566.50367812422918994210763048m, sut.Data[2].PresentValueOfExpectedCashflows); 
            Assert.Equal(-433.49632187577081005789236952m, sut.Data[2].NetPresentValue);

            Assert.Equal(563.31260451425982957437409626m, sut.Data[3].PresentValueOfExpectedCashflows); 
            Assert.Equal(-436.68739548574017042562590374m, sut.Data[3].NetPresentValue); 

            Assert.Equal(560.14905827347544263114208763m, sut.Data[4].PresentValueOfExpectedCashflows); 
            Assert.Equal(-439.85094172652455736885791237m, sut.Data[4].NetPresentValue); 
        }

        [Fact]
        public void ComputeNPVs_UpperLowerBoundValidation_Test()
        {
            // Arrange
            var npvService = new NpvService();

            // Act
            var sut = npvService.ComputeNPVs(-1000, 2, 1, 0.25m, new List<decimal>());

            // Assert
            Assert.NotEmpty(sut.Errors);
            Assert.Equal(sut.Errors[0].Code, ErrorCodes.NPVServiceError006);
        }
    }
}
