#!/usr/bin/perl

use strict;
use File::Copy;
use File::Path;
use Shell;

my $src = "c:\\http_iis\\dnn\\DotNetNuke_05.06.01\\DesktopModules\\FlashRotator\\avt.DynamicFlashRotator.Dnn\\";
my $dst = "c:\\work\\_releases\\DynamicRotator\\1.1.0\\";
my $packageName = "avt.DynamicRotator.DNN.1.1.0_";


# 1. Copy ascx files and remove codefile attr if it exists

my @files = <$src*.ascx>;
foreach my $file (@files) {
    $file = substr($file, rindex($file, '\\')+1);
    print "$file > $dst$file\n";
    copy($src.$file, $dst.$file)
        or die "File $file cannot be copied to $dst.";
    
    # let's read the file
    open FILE, "<",$dst.$file or die $!;
    my $fileBody;
    read(FILE, $fileBody, -s $dst.$file);
    close FILE;
    
    # remove codefile attribute
    open FILE, ">",$dst.$file or die $!;
    $fileBody =~ s/CodeFile\s*=\s*".*?"\s*//i;
    print FILE $fileBody;
    close FILE;
}

my @files = <$src*.aspx>;
foreach my $file (@files) {
    $file = substr($file, rindex($file, '\\')+1);
    print "$file > $dst$file\n";
    copy($src.$file, $dst.$file)
        or die "File $file cannot be copied to $dst.";
    
    # let's read the file
    open FILE, "<",$dst.$file or die $!;
    my $fileBody;
    read(FILE, $fileBody, -s $dst.$file);
    close FILE;
    
    # remove codefile attribute
    open FILE, ">",$dst.$file or die $!;
    $fileBody =~ s/CodeFile\s*=\s*".*?"\s*//i;
    print FILE $fileBody;
    close FILE;
}

print "  > copy res\n";
system ("xcopy \"".$src."res\" \"".$dst."res\" /e /s /y");

print "  > copy act\n";
system ("xcopy \"".$src."act\" \"".$dst."act\" /e /s /y");

print "  > copy assemblies\n";
copy($src."bin/release/avt.DynamicFlashRotator.Net.dll", $dst."avt.DynamicFlashRotator.Net.dll");
copy($src."bin/release/avt.DynamicFlashRotator.WebManage.dll", $dst."avt.DynamicFlashRotator.WebManage.dll");
copy($src."bin/release/avt.DynamicFlashRotator.Dnn.dll", $dst."avt.DynamicFlashRotator.Dnn.dll");


# pack resources
print("  > packing resources...");
chdir($dst);
system("\"c:\\tools\\infozip\\zip.exe\" -r Resources.zip act res Activation.aspx ManageRotator.aspx AdminApi.aspx Rotator.ascx License.txt ManageRotator.aspx >>..\\log.txt");
print("done\n");

print("> packing install kit...");
#chdir($dst."..\\");
system("\"c:\\tools\\infozip\\zip.exe\" -D ..\\".$packageName.".zip * -x *.aspx *.ascx >>..\\log.txt");
print("done\n");



sub copyAllFiles
{
    my ($srcFolder, $dstFolder, @ext) = @_;
    
    mkpath($dstFolder);
    for my $iext (@ext) {
        my @files = <$srcFolder*.$iext>;
        foreach my $file (@files) {
            $file = substr($file, rindex($file, '\\')+1);
            copy($srcFolder.$file, $dstFolder.$file);
        }
    }
}