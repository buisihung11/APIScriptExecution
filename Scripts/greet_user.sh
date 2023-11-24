#!/bin/bash

# Check if the user provided a name as an argument
if [ $# -eq 0 ]; then
  echo "Usage: $0 <your_name>"
  exit 1
fi

# Capture the user's name from the command line argument
user_name=$1

# Print a greeting message
echo "Hello, $user_name!"
