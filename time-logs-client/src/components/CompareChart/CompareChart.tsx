import { Chart, ReactGoogleChartProps } from "react-google-charts";

interface CompareChartProps extends ReactGoogleChartProps {
  title: string;
  resourceLabel: string;
  metricLabel: string;
  data: (string | number)[][];
  comparedData?: {
    label: string;
    value: number;
  };
}

const CompareChart = ({
  title,
  resourceLabel,
  metricLabel,
  data,
  comparedData,
  ...props
}: CompareChartProps) => {
  const { chartType, ...rest } = props;

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
        title,
        vAxis: { title: metricLabel },
        hAxis: { title: resourceLabel },
        seriesType: "bars",
        series: { 1: { type: "line" } },
      }}
      {...rest}
    />
  );
};

export default CompareChart;
