import { Chart } from "react-google-charts";

interface BarChartProps {
  resourceLabel: string;
  metricLabel: string;
  data: (string | number)[][];
  comparedData?: {
    label: string;
    value: number;
  };
}

const BarChart = ({
  resourceLabel,
  metricLabel,
  data,
  comparedData,
}: BarChartProps) => {
  return (
    <Chart
      chartType="ComboChart"
      data={[
        [resourceLabel, metricLabel, comparedData?.label].filter(
          (v) => v !== undefined,
        ),
        ...data.map((d) =>
          [...d, comparedData?.value].filter((v) => v !== undefined),
        ),
      ]}
      width="100%"
      height="400px"
      legendToggle
      options={{
        title: "Top",
        vAxis: { title: "Cups" },
        hAxis: { title: "Month" },
        seriesType: "bars",
        series: { 1: { type: "line" } },
      }}
    />
  );
};

export default BarChart;
