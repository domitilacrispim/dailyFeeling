
import ApexCharts from 'react-apexcharts';

export default function Chart() {
  const options = {
    chart: {
      id: 'basic-line'
    },
    xaxis: {
      type: 'category'
    },
    yaxis:{
        labels: {
            formatter: (value) => {      
                const imageMap = {
                    5: '../../imgs/very_happy.png',  // Substitua pelos seus URLs
                    4: '../../imgs/happy.png',
                    3:'../../imgs/medium.png',
                    2:'../../imgs/sad.png',
                    1:'../../imgs/very_sad.png',
                  };        
              return `<img src="${imageMap[value]}" alt="yaxis-image"
               style="width: 20px; height: 20px;" />`; 
            }
          }
    },
    stroke: {
      curve: 'smooth'
    }
  };

  const series = [{
    name: 'Days',
    data: [
      { x: 'Sunday', y: 1 },
      { x: 'Monday', y: 2 },
      { x: 'Tuesday', y: 3 },
      { x: 'Wednesday', y: 1 },
      { x: 'Thursday', y: 5 },
      { x: 'Friday', y: 4},
      { x: 'Saturday', y: 4}


    ]
  }];

  return (
    <>
      <ApexCharts
        options={options}
        series={series}
        type="line"
        width={640}
        height={400}
      />
    </>
  );
}
