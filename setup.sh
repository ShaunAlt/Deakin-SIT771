#!/usr/bin/env bash
# =============================================================================
# Created by: Shaun Altmann
# =============================================================================
# SIT771 Setup Script
#  Contains initialization functionality which helps to simplify the creation,
#  modification, and running of different C# programs within this repository.
#
# How to Use:
# - Go to the directory where this script is located, and type the following:
#   source ./setup.sh
# - You will now be able to access any of the constants and functions
#   implemented in this script.
# =============================================================================

# =============================================================================
# Defining Constants
# =============================================================================
SCRIPT_DIR=$(dirname "$( realpath "$0" )") # directory containing this script

# =============================================================================
# Program Creation
# - Creates a new directory within the repository, redirects into it, and then
#   creates a new C# project within it.
#
# Parameters:
# - $0 : Defaults to function name. Not Implemented.
# - $1 : Name of the new project. Should not be a pre-existing folder.
#
# Returns:
# - `0` : Success.
# - `1` : ERROR - Parameters were invalid.
# - `2` : ERROR - Directory already existed.
# - `3` : ERROR - Failed to create project.
#
# How to Use:
# - Type the following line into terminal after sourcing this setup file.
#   >>> create_project <PROJECT_NAME>
# =============================================================================
create_project() {
    echo "| Creating a New Project"

    # validating parameters
    echo "| - Validating Parameters"
    if [ $# -ne 1 ]; then
        echo "| - ERROR: Invalid Parameters"
        echo "| Failed"
        return 1
    fi

    # redirect to parent directory
    echo "| - Redirect to Parent Directory"
    cd $SCRIPT_DIR

    # validate that the new folder doesn't already exist
    echo "| - Validate Folder Name"
    if [ -d "./$1" ]; then
        echo "| - ERROR: $1 already exists!"
        echo "| Failed"
        return 2
    fi

    # create the new directory
    echo "| - Create New Directory"
    mkdir $1

    # redirect to the new directory
    echo "| - Redirect To New Directory"
    cd $1

    # create new C# project
    echo "| - Create New C# Project"
    echo "| | - Creating Project"
    skm dotnet new console > /dev/null
    echo "| | - Restoring SKM"
    skm dotnet restore > /dev/null

    # project created successfully
    echo "| Success"
    return 0
}

# =============================================================================
# Program Run
# - If given a parameter, runs the program from the specified directory.
#   Otherwise if no parameter is given, runs the program from the current
#   directory.
#
# Parameters:
# - $0 : Defaults to function name. Not Implemented.
# - $1 : Optional name of the directory containing the program to be run. If
#   not specified, the current directory is used.
#
# Returns:
# - `0` : Success.
# - `1` : ERROR - No directory specified, but current directory failed to find
#         the program.
# - `2` : ERROR - Directory not found.
# - `3` : ERROR - Directory found, but failed to find the program.
# - `4` : ERROR - Parameters were invalid.
#
# How to Use:
# - Type one of the following lines into the terminal after sourcing this setup
#   file.
#   >>> run_program
#   >>> run_program <DIRECTORY_NAME>
# =============================================================================
run_program()
{
    # no parameters - therefore use current directory
    if [ $# -eq 0 ]; then
        # validate that `Program.cs` exists
        if [ ! -f "./Program.cs" ]; then
            echo "ERROR: No Program.cs found in current directory"
            return 1
        fi
    # directory specified - use that
    elif [ $# -eq 1 ]; then
        # return to base repository
        cd $SCRIPT_DIR

        # validate that the directory exists
        if [ ! -d "./$1" ]; then
            echo "ERROR: Directory $1 not found"
            return 2
        fi

        # validate that `Program.cs` exists in the directory
        if [ ! -f "./$1/Program.cs" ]; then
            echo "ERROR: No Program.cs found in directory $1"
            return 3
        fi

        # redirect to the directory
        cd $1
    # invalid parameters
    else
        echo "ERROR: Invalid Parameters"
        return 4
    fi

    # run the program
    skm dotnet run
    return 0
}

# =============================================================================
# End of File
# =============================================================================
