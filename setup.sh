#!/usr/bin/env bash
# =============================================================================
# Created by: Shaun Altmann
# =============================================================================
# SIT771 Setup Script
#  Contains initialization functionality which helps to simplify the creation,
#  modification, and running of different C# programs within this repository.
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
#   create_project <PROJECT_NAME>
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
    if [ -d ./$1 ]; then
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
# - Creates a new directory within the repository, redirects into it, and then
#   creates a new C# project within it.
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
# - None
# =============================================================================
run_program()
{
    # redirect to parent directory
    cd $SCRIPT_DIR

    # validate that the directory exists
    if [! -d $1 ]; then
    fi
}

# skm dotnet run

# =============================================================================
# End of File
# =============================================================================
