import ReactDateRangePicker from "react-daterange-picker";
import "react-daterange-picker/dist/css/react-calendar.css";

import originalMoment from "moment";
import { DateRange, extendMoment } from "moment-range";

import DeleteIcon from "@mui/icons-material/Delete";
import { Box, Button, Popper, Typography } from "@mui/material";

import useDateRangePicker from "./useDateRangePicker";

const moment = extendMoment(originalMoment as any);

interface DateRangePickerProps extends ReactDateRangePicker.BaseProps {
  onChange?(dateRange: DateRange | null): void /* Used from dynamic forms. */;
}

const DateRangePicker = ({
  onChange = () => {},
  value, // Remove the default value from ReactDateRangePicker.BaseProps to not cause conflicts with the custom defaultValue.
  ...props
}: DateRangePickerProps) => {
  const {
    isOpen,
    handleOpenChange,
    dateRange,
    handleDateRangeOnSelect,
    handleResetOnClick,
  } = useDateRangePicker(onChange);

  return (
    <Box
      style={{
        backgroundColor: "rgba(0, 0, 0, 0.06)",
        borderRadius: "4px",
        height: "36px",
        marginBottom: "30px",

        background: "#fff",
        zIndex: "700000",
      }}
    >
      <Button onClick={handleOpenChange}>
        {dateRange !== null ? (
          <Typography component="span">
            {`${dateRange.start.format("YYYY-MM-DD")} - ${dateRange.end
              .endOf("day")
              .format("YYYY-MM-DD")}`}
          </Typography>
        ) : (
          <Typography>YYYY-MM-DD - YYYY-MM-DD</Typography>
        )}
      </Button>

      <Button disabled={dateRange === null} onClick={handleResetOnClick}>
        <DeleteIcon />
      </Button>

      <Popper
        sx={{
          background: "#fff",
          zIndex: "700000",
          marginTop: "3%",
        }}
        open={isOpen}
      >
        <ReactDateRangePicker
          selectionType="range"
          firstOfWeek={1}
          value={dateRange || undefined}
          onSelect={handleDateRangeOnSelect}
          {...props}
        />
      </Popper>
    </Box>
  );
};

export default DateRangePicker;
