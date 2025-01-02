<?xml version="1.0" encoding="UTF-8"?>
<project xmlns="http://maven.apache.org/POM/4.0.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://maven.apache.org/POM/4.0.0 http://maven.apache.org/maven-v4_0_0.xsd">
    <modelVersion>4.0.0</modelVersion>

    <parent>
        <groupId>org.sonatype.oss</groupId>
        <artifactId>oss-parent</artifactId>
        <version>7</version>
    </parent>

    <packaging>jar</packaging>
    <groupId>org.webjars</groupId>
    <artifactId>angular-ui-utils</artifactId>
    <version>0.2.3-SNAPSHOT</version>
    <name>Angular Ui Utils</name>
    <description>WebJar for Angular UI Utils</description>
    <url>http://webjars.org</url>

    <properties>
        <project.build.sourceEncoding>UTF-8</project.build.sourceEncoding>
        <angularui.utils.version>0.2.2</angularui.utils.version>
        <angularui.utils.sourceUrl>https://raw.github.com/angular-ui/ui-utils/v${angularui.utils.version}</angularui.utils.sourceUrl>
        <destDir>${project.build.outputDirectory}/META-INF/resources/webjars/${project.artifactId}/${angularui.utils.version}</destDir>
        <requirejs>
{
                "paths": {
                    "angular-ui-utils": "ui-utils"
    },
                "shim": {
                    "angular-ui-utils": [ "angular", "jquery" ]
    }
    }
        </requirejs>
    </properties>

    <licenses>
        <license>
            <name>MIT License</name>
            <url>https://github.com/angular-ui/ui-utils/blob/master/LICENSE</url>
            <distribution>repo</distribution>
        </license>
    </licenses>

    <dependencies>
        <dependency>
            <groupId>org.webjars</groupId>
            <artifactId>angularjs</artifactId>
            <version>1.2.27</version>
        </dependency>
        <dependency>
            <groupId>org.webjars</groupId>
            <artifactId>jquery</artifactId>
            <version>1.11.1</version>
        </dependency>
    </dependencies>

    <scm>
        <url>https://github.com/webjars/angular-ui-utils</url>
        <connection>scm:git:https://github.com/webjars/angular-ui-utils.git</connection>
        <developerConnection>scm:git:https://github.com/webjars/angular-ui-utils.git</developerConnection>
        <tag>HEAD</tag>
    </scm>

    <developers>
        <developer>
            <id>smallg</id>
            <name>Small Guo</name>
            <email>small.guo@enovation.com.cn</email>
        </developer>
        <developer>
            <id>jamesward</id>
            <name>James Ward</name>
            <email>james@jamesward.com</email>
        </developer>
    </developers>

    <build>
        <plugins>
            <plugin>
                <groupId>org.codehaus.mojo</groupId>
                <artifactId>wagon-maven-plugin</artifactId>
                <version>1.0-beta-4</version>
                <executions>
                    <execution>
                        <id>ui-utils.js</id>
                        <phase>process-resources</phase>
                        <goals>
                            <goal>download-single</goal>
                        </goals>
                        <configuration>
                            <url>${angularui.utils.sourceUrl}</url>
                            <fromFile>ui-utils.js</fromFile>
                            <toDir>${destDir}</toDir>
                        </configuration>
                    </execution>
                    <execution>
                        <id>ui-utils.min.js</id>
                        <phase>process-resources</phase>
                        <goals>
                            <goal>download-single</goal>
                        </goals>
                        <configuration>
                            <url>${angularui.utils.sourceUrl}</url>
                            <fromFile>ui-utils.min.js</fromFile>
                            <toDir>${destDir}</toDir>
                        </configuration>
                    </execution>
                    <execution>
                        <id>ui-utils-ieshiv.js</id>
                        <phase>process-resources</phase>
                        <goals>
                            <goal>download-single</goal>
                        </goals>
                        <configuration>
                            <url>${angularui.utils.sourceUrl}</url>
                            <fromFile>ui-utils-ieshiv.js</fromFile>
                            <toDir>${destDir}</toDir>
                        </configuration>
                    </execution>
                    <execution>
                        <id>ui-utils-ieshiv.min.js</id>
                        <phase>process-resources</phase>
                        <goals>
                            <goal>download-single</goal>
                        </goals>
                        <configuration>
                            <url>${angularui.utils.sourceUrl}</url>
                            <fromFile>ui-utils-ieshiv.min.js</fromFile>
                            <toDir>${destDir}</toDir>
                        </configuration>
                    </execution>
                </executions>
            </plugin>
            <plugin>
                <groupId>org.apache.maven.plugins</groupId>
                <artifactId>maven-release-plugin</artifactId>
                <version>2.5.1</version>
            </plugin>

            <plugin>
                <groupId>org.sonatype.plugins</groupId>
                <artifactId>nexus-staging-maven-plugin</artifactId>
                <version>1.6.5</version>
                <extensions>true</extensions>
                <configuration>
                    <serverId>sonatype-nexus-staging</serverId>
                    <nexusUrl>https://oss.sonatype.org/</nexusUrl>
                    <autoReleaseAfterClose>true</autoReleaseAfterClose>
                </configuration>
            </plugin>
        </plugins>
        <resources>
            <resource>
               <directory>${project.basedir}/src/main/resources</directory>
                <targetPath>${destDir}</targetPath>
            </resource>
        </resources>
    </build>
</project>