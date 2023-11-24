#!/bin/bash

# Check if two numbers are provided as arguments
if [ $# -ne 2 ]; then
  echo "Usage: $0 <number1> <number2>"
  exit 1
fi

# Capture the numbers from the command line arguments
number1=$1
number2=$2

# Perform the sum calculation
sum=$((number1 + number2))

# Print the result
echo "The sum of $number1 and $number2 is: $sum"
