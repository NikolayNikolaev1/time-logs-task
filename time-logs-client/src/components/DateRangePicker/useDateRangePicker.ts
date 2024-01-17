import { useState } from "react";

import { DateRange } from "moment-range";

const useDateRangePicker = (
  onChange: (dateRange: DateRange | null) => void,
) => {
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const [dateRange, setDateRange] = useState<DateRange | null>(null);

  const handleDateRangeOnSelect = (selectedDateRange: DateRange) => {
    setIsOpen(false);
    setDateRange(selectedDateRange);
    onChange(selectedDateRange);
  };

  // Removes the selected dateRange and leaves the input empty.
  const handleResetOnClick = () => {
    setDateRange(null);
    onChange(null);
  };

  const handleOpenChange = () => setIsOpen((oldState) => !oldState);

  return {
    dateRange,
    handleDateRangeOnSelect,
    handleResetOnClick,
    isOpen,
    handleOpenChange,
  };
};

export default useDateRangePicker;
