<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:template match="root">
        <Accounts>
            <xsl:for-each select="rows">
                <Account>
                    <AccountId>
                        <xsl:value-of select="Id" />
                    </AccountId>
                </Account>
            </xsl:for-each>
        </Accounts>
    </xsl:template>
</xsl:stylesheet>